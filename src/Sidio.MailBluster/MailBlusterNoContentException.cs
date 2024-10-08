﻿using Sidio.MailBluster.Responses;

namespace Sidio.MailBluster;

[Serializable]
internal sealed class MailBlusterNoContentException : Exception
{
    public MailBlusterNoContentException(MailBlusterResponse response)
        : base(response.Message)
    {
    }
}