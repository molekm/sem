﻿// Copyright 2016, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Google.Api.Ads.AdWords.Util.Reports
{
    /// <summary>
    /// A class to hold two strings, the column name and the value in that column for a row in
    /// a report.
    /// </summary>
    public class ColumnValuePair
    {
        /// <summary>
        /// The name of the column.
        /// </summary>
        public readonly string ColName;

        /// <summary>
        /// The value in this column.
        /// </summary>
        public readonly string Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnValuePair" /> class.
        /// </summary>
        /// <param name="colName">The column name</param>
        /// <param name="value">The column value</param>
        public ColumnValuePair(string colName, string value)
        {
            this.ColName = colName;
            this.Value = value;
        }
    }
}
