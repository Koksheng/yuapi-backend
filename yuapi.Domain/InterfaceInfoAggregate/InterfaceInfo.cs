using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuapi.Domain.Common.Models;
using yuapi.Domain.InterfaceInfoAggregate.ValueObjects;

namespace yuapi.Domain.InterfaceInfoAggregate
{
    public sealed class InterfaceInfo : AggregateRoot<InterfaceInfoId>
    {
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string requestHeader { get; set; }
        public string responseHeader { get; set; }
        public string userId { get; set; }
        public int status { get; set; }
        public string method { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public int isDelete { get; set; }


        private InterfaceInfo(
            InterfaceInfoId interfaceInfoId,
            string name,
            string description,
            string url,
            string requestHeader,
            string responseHeader,
            string userId,
            int status,
            string method,
            DateTime createTime,
            DateTime updateTime,
            int isDelete)
            : base(interfaceInfoId)
        {
            name = name;
            description = description;
            url = url;
            requestHeader = requestHeader;
            responseHeader = responseHeader;
            userId = userId;
            status = status;
            method = method;
            createTime = createTime;
            updateTime = updateTime;
            isDelete = isDelete;
        }

        public static InterfaceInfo Create(
            string name,
            string description,
            string url,
            string requestHeader,
            string responseHeader,
            string userId,
            int status,
            string method)
        {
            return new(
                InterfaceInfoId.CreateUnique(),
                name,
                description,
                url,
                requestHeader,
                responseHeader,
                userId,
                status,
                method,
                DateTime.UtcNow,
                DateTime.UtcNow,
                0);
        }
    }
}