using GoHorse.Core.Application.Models.Headless;
using GoHorse.Core.Application.Models.Headless.Selectors;
namespace GoHorse.Core.Application.Contracts.Providers;

public interface HeadlessProvider
{
  Task open(Func<Task> Handle, Open data);
  Task GoTo(GoTo data);
  Task<bool> Click(Click data);
  Task<bool> FillInput(FillInput data);
  Task<bool> GetElement(GetElement data);
  Task<IEnumerable<T>> GetElementsByEvaluateFunction<T>(GetElementsByEvaluateFunction data) where T : class;
  Task<string?> GetText(GetText data);
  Task<string?> GetAttribute(GetAttribute data);
  Task Reload();
}
