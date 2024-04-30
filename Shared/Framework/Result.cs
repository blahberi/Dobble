using System;

namespace Dobble.Shared.Framework
{
	/// <summary>
	/// A result that can be either successful or failed
	/// </summary>
	public class Result
	{
		private readonly string errorMessage;

		/// <summary>
		/// Constructor for a result
		/// </summary>
		/// <param name="success"></param>
		/// <param name="errorMessage"></param>
		protected Result(bool success, string errorMessage)
		{
			this.Success = success;
			this.errorMessage = errorMessage;
		}


		/// <summary>
		/// Whether the result is successful
		/// </summary>
		public bool Success { get; }

		/// <summary>
		/// The error message if the result is not successful
		/// </summary>
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
		
		/// <summary>
		/// Creates a successful result
		/// </summary>
		/// <returns>A successful result</returns>
		public static Result SuccessResult()
		{
			return new Result(true, errorMessage: null);
		}

		/// <summary>
		/// Creates a failed result
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns>A failed result with the errorMessage</returns>
		public static Result FailureResult(string errorMessage)
		{
			return new Result(false, errorMessage);
		}
	}

	/// <summary>
	/// A result with a value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Result<T> : Result
	{
		private readonly T value;

		/// <summary>
		/// Constructor for a result
		/// </summary>
		/// <param name="success"></param>
		/// <param name="value"></param>
		/// <param name="errorMessage"></param>
		private Result(bool success, T value, string errorMessage)
			: base(success, errorMessage)
		{
			this.value = value;
		}

		/// <summary>
		/// The value of the result
		/// </summary>
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

		/// <summary>
		/// Creates a successful result with a value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Result<T> SuccessResult(T value)
		{
			return new Result<T>(true, value, errorMessage: null);
		}
		
		/// <summary>
		///	Creates a failed result with a value
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static new Result<T> FailureResult(string errorMessage)
		{
			return new Result<T>(false, value: default, errorMessage);
		}
	}
}
