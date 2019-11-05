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
    /// This example gets all product package items belonging to a product package.
    /// </summary>
    public class GetProductPackageItemsForProductPackage : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets all product package items belonging to a product package.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetProductPackageItemsForProductPackage codeExample =
                new GetProductPackageItemsForProductPackage();
            long productPackageId = long.Parse("INSERT_PRODUCT_PACKAGE_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), productPackageId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get product package items. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long productPackageId)
        {
            using (ProductPackageItemService productPackageItemService =
                user.GetService<ProductPackageItemService>())
            {
                // Create a statement to select product package items.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("productPackageId = :productPackageId").OrderBy("id ASC").Limit(pageSize)
                    .AddValue("productPackageId", productPackageId);

                // Retrieve a small amount of product package items at a time, paging through until
                // all product package items have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ProductPackageItemPage page =
                        productPackageItemService.getProductPackageItemsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each product package item.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ProductPackageItem productPackageItem in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Product package item with ID {1}, " + "product ID {2}, " +
                                "and product package ID {3} was found.", i++, productPackageItem.id,
                                productPackageItem.productId, productPackageItem.productPackageId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
