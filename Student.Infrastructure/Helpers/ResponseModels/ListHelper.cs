using Student.Infrastructure.Enums;

namespace Student.Infrastructure.Helpers.ResponseModels
{
    public static class ListHelper
    {
        public static void AddMessage(this List<Message<MessageType, string>> obj, MessageType type, string value)
        {
            obj.Add(new Message<MessageType, string>(type, value));
        }
    }
}
