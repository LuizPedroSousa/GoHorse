namespace GoHorse.Core.Domain.Shared;

public class Either<L, R> where L : BaseException
{
  public L left { get; private set; }

  public R right { get; private set; }


  public Either(L left)
  {
    if (right is null)
      this.left = left;
  }

  public Either(R right)
  {
    if (left is null)
      this.right = right;
  }


  public bool IsRight() => left is L && right is null;
  public bool IsLeft() => right is R && left is null;

  public static implicit operator Either<L, R>(R result) => new Either<L, R>(result);
  public static implicit operator Either<L, R>(L exception) => new Either<L, R>(exception);
}