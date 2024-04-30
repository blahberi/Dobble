using System;

namespace Dobble.Shared.Framework
{
	public class Result
	{
		private readonly string errorMessage;

		protected Result(bool success, string errorMessage)
		{
			this.Success = success;
			this.errorMessage = errorMessage;
		}

		public bool Success { get; }

		public string ErrorMessage
		{
			get
			{
				if (this.Success)
				{
					throw new InvalidOperationException("ErrorMessage is invalid when result is successful");
				}

				return this.errorMessage;
			}
		}

		public static Result SuccessResult()
		{
			return new Result(true, errorMessage: null);
		}

		public static Result FailureResult(string errorMessage)
		{
			return new Result(false, errorMessage);
		}
	}

	public class Result<T> : Result
	{
		private readonly T value;

		private Result(bool success, T value, string errorMessage)
			: base(success, errorMessage)
		{
			this.value = value;
		}

		public T Value
		{
			get
			{
				if (!this.Success)
				{
					throw new InvalidOperationException("Value is invalid when result is non successful");
				}

				return this.value;
			}
		}

		public static Result<T> SuccessResult(T value)
		{
			return new Result<T>(true, value, errorMessage: null);
		}

		public static new Result<T> FailureResult(string errorMessage)
		{
			return new Result<T>(false, value: default, errorMessage);
		}
	}
}
