﻿using System;
using System.Linq;
using JCSoft.WX.Framework.Models.ApiRequests;
using JCSoft.WX.Framework.Models.ApiResponses;
using Xunit;
using JCSoft.WX.Framework.Models;

namespace FrameworkCoreTest
{
    public class SNSUserInfoTest : MockPostApiBaseTest<SnsUserInfoRequest, SnsUserInfoResponse>
    {
        [Fact]
        public void MockSNSUserInfoTest()
        {
            MockSetup(false);
            var response = mock_client.Object.Execute(Request);
            Assert.Equal("NICKNAME", response.NickName);
            Assert.Equal("1", response.Sex);
            Assert.Equal(2, response.Privilege.Count());
        }

        [Fact]
        public void MockSNSUserInfoErrorTest()
        {
            MockSetup(true);
            var response = mock_client.Object.Execute(Request);
            Console.WriteLine(response);
        }

        protected override SnsUserInfoRequest InitRequestObject()
        {
            return new SnsUserInfoRequest
            {
                Lang = Language.CN,
                OAuthToken = "ACCESS_TOKEN",
                OpenId = "OPENID"
            };
        }

        protected override string GetReturnResult(bool errResult)
        {
            if (errResult)
                return "{\"errcode\":40003,\"errmsg\":\" invalid openid \"}";

            return @"{
                       ""openid"":"" OPENID"",
                       ""nickname"": ""NICKNAME"",
                       ""sex"":""1"",
                       ""province"":""PROVINCE"",
                       ""city"":""CITY"",
                       ""country"":""COUNTRY"",
                        ""headimgurl"":""http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/46"", 
	                    ""privilege"":[
	                    ""PRIVILEGE1"",
	                    ""PRIVILEGE2""
                        ]
                    }";
        }

        public override SnsUserInfoResponse GetResponse()
        {
            return mock_client.Object.Execute(Request);
        }
    }
}
