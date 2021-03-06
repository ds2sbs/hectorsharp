﻿using System;
using Apache.Cassandra;

namespace HectorSharp
{
	internal partial class Keyspace
	{
		interface IOperation
		{
			void Execute(Cassandra.Iface client);
			bool HasError { get; }
			NotFoundException Error { get; }
			ClientCounter FailCounter { get; }
		}

		#region Operation<T>

		/// <summary>
		/// Defines the interface of an operation performed on cassandra
		/// </summary>
		/// <typeparam name="T">
		/// The result type of the operation (if it has a result), such as the
		/// result of get_count or get_column
		/// </typeparam>
		class Operation<T> : IOperation
		{
			public Func<Cassandra.Iface, T> Handler { get; set; }
			public T Result { get; private set; }
			public bool HasError { get { return Error != null; } }
			public NotFoundException Error { get; set; }
			public ClientCounter FailCounter { get; private set; }

			public Operation(ClientCounter failCounter)
			{
				this.FailCounter = failCounter;
			}

			public Operation(ClientCounter failCounter, Func<Cassandra.Iface, T> handler)
				: this(failCounter)
			{
				this.Handler = handler;
			}

			/// <summary>
			/// Performs the operation on the given cassandra instance.
			/// </summary>
			/// <param name="client">client</param>
			/// <returns>null if no execute handler</returns>
			public void Execute(Cassandra.Iface client)
			{
				if (Handler == null)
					throw new ApplicationException("Execution Handler was null");

				Result = Handler(client);
			}
		}
		#endregion

		#region VoidOperation
		/// <summary>
		/// Defines the interface of a void operation performed on cassandra
		/// </summary>
		class VoidOperation : IOperation
		{
			public Action<Cassandra.Iface> Handler { get; set; }

			public bool HasError { get { return Error != null; } }
			public NotFoundException Error { get; set; }
			public ClientCounter FailCounter { get; private set; }

			public VoidOperation(ClientCounter failCounter)
			{
				this.FailCounter = failCounter;
			}

			public VoidOperation(ClientCounter failCounter, Action<Cassandra.Iface> handler)
				: this(failCounter)
			{
				this.Handler = handler;
			}

			/// <summary>
			/// Performs the operation on the given cassandra instance
			/// </summary>
			/// <param name="cassandra">client</param>
			public void Execute(Cassandra.Iface client)
			{
				if (Handler == null)
					throw new ApplicationException("Execution Handler was null");

				Handler(client);
			}
		}
		#endregion
	}
}
