// Copyright 2019 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201905;
using Google.Api.Ads.AdManager.v201905;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201905
{
    /// <summary>
    /// This example gets all exchange rates.
    /// </summary>
    public class GetAllExchangeRates : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all exchange rates."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllExchangeRates codeExample = new GetAllExchangeRates();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get exchange rates. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ExchangeRateService exchangeRateService = user.GetService<ExchangeRateService>())
            {
                // Create a statement to select exchange rates.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder =
                    new StatementBuilder().OrderBy("id ASC").Limit(pageSize);

                // Retrieve a small amount of exchange rates at a time, paging through until all
                // exchange rates have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ExchangeRatePage page =
                        exchangeRateService.getExchangeRatesByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each exchange rate.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ExchangeRate exchangeRate in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Exchange rate with ID {1}, " + "currency code \"{2}\", " +
                                "direction \"{3}\", " + "and exchange rate {4} was found.", i++,
                                exchangeRate.id, exchangeRate.currencyCode, exchangeRate.direction,
                                exchangeRate.exchangeRate);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
