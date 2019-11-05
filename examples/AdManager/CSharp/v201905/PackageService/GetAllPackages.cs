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
    /// This example gets all packages.
    /// </summary>
    public class GetAllPackages : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all packages."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllPackages codeExample = new GetAllPackages();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get packages. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (PackageService packageService = user.GetService<PackageService>())
            {
                // Create a statement to select packages.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder =
                    new StatementBuilder().OrderBy("id ASC").Limit(pageSize);

                // Retrieve a small amount of packages at a time, paging through until all
                // packages have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    PackagePage page =
                        packageService.getPackagesByStatement(statementBuilder.ToStatement());

                    // Print out some information for each package.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (Package pkg in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Package with ID {1}, name \"{2}\", and proposal id {3} was " +
                                "found.",
                                i++, pkg.id, pkg.name, pkg.proposalId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
