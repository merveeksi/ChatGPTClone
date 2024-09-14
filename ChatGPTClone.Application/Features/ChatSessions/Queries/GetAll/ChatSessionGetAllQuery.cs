using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ChatGPTClone.Application.Features.ChatSessions.Queries.GetAll
{
    public class ChatSessionGetAllQuery : IRequest<List<ChatSessionGetAllDto>>
    {
        public ChatSessionGetAllQuery()
        {
            
        }
    }
}