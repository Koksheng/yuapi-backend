namespace yuapi.Contracts.UserInterfaceInfo
{
    public record UpdateUserInterfaceInfoRequest(
        int id,

        /**
         * 总调用次数
         */
        int totalNum,

        /**
         * 剩余调用次数
         */
        int leftNum,

        /**
         * 0-正常，1-禁用
         */
        int status);
}
