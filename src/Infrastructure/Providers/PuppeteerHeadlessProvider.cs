using System.Runtime.InteropServices.ComTypes;
using GoHorse.Application.Contracts.Providers;
using GoHorse.Application.Models.Headless.Selectors;
using GoHorse.Application.Models.Headless;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using Newtonsoft.Json.Linq;

namespace GoHorse.Infrastructure.Providers.Headless;

public class PuppeteerHeadlessProvider : HeadlessProvider
{
  private static LaunchOptions getLaunchOptions(bool headless, ViewPortOptions? viewPort = default) => new LaunchOptions()
  {
    Headless = headless,
    Args = new[] { "--load-extensions", "--start-maximized" },
    IgnoredDefaultArgs = new[] {
        "--disable-extensions",
        "--enable-automation",
        "--disable-web-security",
        "--disable-features=IsolateOrigins,site-per-process",
    },
    DefaultViewport = {
       Width = viewPort is not null ? viewPort.Width : 1366,
       Height = viewPort is not null ? viewPort.Width :  768,
    },
  };

  private IPage pageInstance { get; set; }

  #region Page commands and queries
  public async Task open(Func<Task> Handle, Open data)
  {
    using var browserFetch = new BrowserFetcher();
    await browserFetch.DownloadAsync();

    using var browser = await Puppeteer.LaunchAsync(getLaunchOptions(headless: data.headless));
    pageInstance = await browser.NewPageAsync();

    await pageInstance.SetUserAgentAsync("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36");
    await pageInstance.SetExtraHttpHeadersAsync(
      new Dictionary<string, string>() {
        {"accept-language", "en-US,en;q=0.9,hy;q=0.8"}
      }
    );

    await Handle();
  }

  public async Task GoTo(GoTo data)
  {
    var page = pageInstance;

    if (data.timeout != null) await page.WaitForTimeoutAsync((int)data.timeout);
    await pageInstance.GoToAsync(data.url, new NavigationOptions
    {
      WaitUntil = new[] { WaitUntilNavigation.DOMContentLoaded },
      Timeout = data.navigationTimeout
    });
  }


  public async Task Reload()
  {
    await pageInstance.ReloadAsync();
  }

  private async Task<IFrame> GetFrame(int maxSelectorTimeout)

  {
    var page = pageInstance;

    string iframeSelector = "iframe[id=\"contentFrame\"]";
    var waitForOptions =
            new WaitForSelectorOptions
            {
              Timeout = maxSelectorTimeout
            };

    await page.WaitForSelectorAsync(
       iframeSelector,
  waitForOptions
  );

    var iframeHandle = await page.QuerySelectorAsync(iframeSelector);

    var iframe = await iframeHandle.ContentFrameAsync();

    await iframe.WaitForSelectorAsync(
      "iframe",
      waitForOptions
  );

    var nestedIframeHandle = await iframe.QuerySelectorAsync("iframe");

    return await nestedIframeHandle.ContentFrameAsync();

  }
  #endregion Page commands and queries


  #region Validations
  private async Task<bool> HandleCheckers(string target, bool isIframe, IFrame frame, IPage page, ActionCheck? checker)
  {
    if (!string.IsNullOrEmpty(checker?.text))
    {

      string text;
      var selector = @$"() => document.querySelector('{target}').textContent";

      if (isIframe)
      {

        text = await frame.EvaluateFunctionAsync<string>(selector);
      }
      else
      {
        text = await page.EvaluateFunctionAsync<string>(selector);
      }

      if (!string.Equals(checker.text, text))
      {
        return false;
      }

    }

    return true;
  }
  #endregion

  #region Keyboard
  public async Task<bool> FillInput(FillInput data)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (data.timeout != null) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }

      await this.HandleCheckers(data.target, data.isIframe, frame, page, data.checker);

      if (data?.isIframe == true)
      {
        await frame.ClickAsync(data.target, new ClickOptions
        {
          ClickCount = 3
        });
        await frame.TypeAsync(data.target, data.content);
      }
      else
      {
        await page.TypeAsync(data.target, data.content);
      }

      return true;
    }
    catch (Exception exception)
    {
      if (data.logError)
        Console.WriteLine($"fill input error - {exception.Message}");
      return false;
    }


  }
  #endregion

  #region Mouse events
  public async Task<bool> Click(Click data)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (data.timeout != null) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
      {
        Timeout = data.timeout
      });

      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }


      await this.HandleCheckers(data.target, data.isIframe, frame, page, data?.checker);

      if (data?.isIframe == true)
      {
        await frame.ClickAsync(data.target);
      }
      else
      {
        await page.ClickAsync(data.target);
      }


      return true;
    }
    catch (Exception exception)
    {
      if (data.logError)
        Console.WriteLine($"Click error - {exception.Message}");

      return false;
    }

  }

  #endregion

  #region Selectors
  public async Task<bool> GetElement(GetElement data)
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (data.timeout != null) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
      {
        Timeout = data.timeout
      });

      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
      }

      await this.HandleCheckers(data.target, data.isIframe, frame, page, data?.checker);

      IElementHandle? element;

      if (data?.isIframe == true)
      {
        element = await frame.QuerySelectorAsync(data.target);

      }
      else
      {
        element = await page.QuerySelectorAsync(data.target);
      }

      if (element is null)
      {
        return false;
      }

      return true;

    }
    catch (Exception exception)

    {
      if (data.logError)
        Console.WriteLine($"getting element error - {exception.Message}");
      return false;
    }
  }
  public async Task<string?> GetText(GetText data)
  {
    var page = pageInstance;
    var frame = page.MainFrame;

    if (data.timeout is int) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.maxSelectorTimeout,
        });
      }
      else
      {
        await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.maxSelectorTimeout
        });
      }



      await this.HandleCheckers(data.target, data.isIframe, frame, page, data?.checker);

      string text;

      if (data?.isIframe == true)
      {
        text = await frame.QuerySelectorAsync(data.target).EvaluateFunctionAsync<string>("element => element.textContent");
      }
      else
      {
        text = await page.QuerySelectorAsync(data.target)
          .EvaluateFunctionAsync<string>("element => element.textContent");
      }
      return text;
    }
    catch (Exception exception)
    {
      if (data.logError)
        Console.WriteLine($"getting text error - {exception.Message}");
      return "";
    }
  }

  public async Task<string?> GetAttribute(GetAttribute data)
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (data.timeout is int) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
        await frame.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }
      else
      {
        await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
        {
          Timeout = data.timeout
        });
      }

      await this.HandleCheckers(data.target, data.isIframe, frame, page, data?.checker);

      string text;

      if (data?.isIframe == true)
      {
        text = await frame.QuerySelectorAsync(data.target)
          .EvaluateFunctionAsync<string>($"element => element.getAttribute('{data.attributeName}')");
      }
      else
      {
        text = await page.QuerySelectorAsync(data.target)
          .EvaluateFunctionAsync<string>($"element => element.getAttribute('{data.attributeName}')");
      }
      return text;
    }
    catch (Exception exception)
    {
      if (data.logError)
        Console.WriteLine($"Getting attribute - {exception.Message}");
      return "";
    }

  }

  public async Task<IEnumerable<T>> GetElementsByEvaluateFunction<T>(GetElementsByEvaluateFunction data) where T : class
  {
    var page = pageInstance;
    var frame = page.MainFrame;


    if (data.timeout != null) await page.WaitForTimeoutAsync((int)data.timeout);

    try
    {
      await page.WaitForSelectorAsync(data.target, new WaitForSelectorOptions
      {
        Timeout = data.timeout
      });


      if (data.isIframe)
      {
        frame = await GetFrame(data.maxSelectorTimeout);
      }

      await this.HandleCheckers(data.target, data.isIframe, frame, page, data?.checker);

      JToken elements;


      if (data?.isIframe == true)
      {
        elements = await frame.EvaluateFunctionAsync(data.script);
      }
      else
      {
        elements = await page.EvaluateFunctionAsync(data.script, data.scriptParams);
      }

      return elements.Select(element => element.ToObject<T>());
    }
    catch (Exception exception)
    {
      if (data.logError)
        Console.WriteLine($"getting element error - {exception.Message}");
      return default(IEnumerable<T>);
    }
  }
  #endregion
}
