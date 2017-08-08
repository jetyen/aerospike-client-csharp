/* 
 * Copyright 2012-2017 Aerospike, Inc.
 *
 * Portions may be licensed to Aerospike, Inc. under one or more contributor
 * license agreements.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */
namespace Aerospike.Client
{
	public sealed class AsyncExists : AsyncSingleCommand
	{
		private readonly ExistsListener listener;
		private readonly Key key;
		private bool exists;

		public AsyncExists(AsyncCluster cluster, Policy policy, Key key, ExistsListener listener) 
			: base(cluster, policy, new Partition(key), true)
		{
			this.listener = listener;
			this.key = key;
		}

		public AsyncExists(AsyncExists other)
			: base(other)
		{
			this.listener = other.listener;
			this.key = other.key;
		}

		protected internal override AsyncCommand CloneCommand()
		{
			return new AsyncExists(this);
		}

		protected internal override void WriteBuffer()
		{
			SetExists(policy, key);
		}

		protected internal override void ParseResult()
		{
			int resultCode = dataBuffer[dataOffset + 5];

			if (resultCode == 0)
			{
				exists = true;
			}
			else
			{
				if (resultCode == ResultCode.KEY_NOT_FOUND_ERROR)
				{
					exists = false;
				}
				else
				{
					throw new AerospikeException(resultCode);
				}
			}
		}

		protected internal override void OnSuccess()
		{
			if (listener != null)
			{
				listener.OnSuccess(key, exists);
			}
		}

		protected internal override void OnFailure(AerospikeException e)
		{
			if (listener != null)
			{
				listener.OnFailure(e);
			}
		}
	}
}
