using System.Collections.Generic;
using Prism.Events;

namespace kata_gof_pattern_eventaggregator_irc
{
    public class MonitoringAppService : ISubscriber<LoginMessage>, ISubscriber<LogoutMessage>
    {
        private readonly IMessageView _messagesView;
        private readonly HashSet<string> _loggedInUsers = new HashSet<string>();

        public MonitoringAppService(IEventAggregator eventAggregator, IMessageView messagesView)
        {
            _messagesView = messagesView;
            eventAggregator.GetEvent<LoginMessageEvent>().Subscribe(Consume);
            eventAggregator.GetEvent<LogoutMessageEvent>().Subscribe(Consume);
        }

        public void Consume(LoginMessage message)
        {
            _loggedInUsers.Add(message.Username);
            _messagesView.Add($"{_loggedInUsers.Count} user(s) online");
        }

        public void Consume(LogoutMessage message)
        {
            _loggedInUsers.Remove(message.Username);
            _messagesView.Add($"{_loggedInUsers.Count} user(s) online");
        }
    }
}