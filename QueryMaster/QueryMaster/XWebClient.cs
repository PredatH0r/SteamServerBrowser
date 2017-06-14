using System;
using System.Net;

namespace QueryMaster
{
  class XWebClient : WebClient
  {
    public XWebClient()
    {
      this.Proxy = null;
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
      var req = base.GetWebRequest(address);
      req.Timeout = 5000;
      return req;
    }
  }
}