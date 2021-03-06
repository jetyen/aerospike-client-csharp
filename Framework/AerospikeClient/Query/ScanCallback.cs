/* 
 * Copyright 2012-2018 Aerospike, Inc.
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
	/// <summary>
	/// This method will be called for each record returned from a scan. The user may throw a 
	/// <seealso cref="Aerospike.Client.AerospikeException.ScanTerminated"/> 
	/// exception if the scan should be aborted.  If any exception is thrown, parallel scan threads
	/// to other nodes will also be terminated and the exception will be propagated back through the
	/// initiating scan call.
	/// <para>
	/// Multiple threads will likely be calling scanCallback in parallel.  Therefore, your scanCallback
	/// implementation should be thread safe.
	/// </para>
	/// </summary>
	/// <param name="key">unique record identifier</param>
	/// <param name="record">container for bins and record meta-data</param>
	/// <exception cref="AerospikeException">if error occurs or scan should be terminated.</exception>
	public delegate void ScanCallback(Key key, Record record);
}
