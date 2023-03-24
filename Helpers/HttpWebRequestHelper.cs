using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BasicEducationDepartmentWeb.Helpers
{
    public class HttpWebRequestHelper
    {
        private readonly BEDLogger _logger = new BEDLogger();
        private readonly string _token;
        private readonly string _authType;

        public HttpWebRequestHelper()
        {
            //
        }

        public HttpWebRequestHelper(string token, string authType = "Bearer")
        {
            _token = token.Trim();
            _authType = authType.Trim();
        }

        public HttpResponseCustom Get(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(url);
            getRequest.Method = "GET";

            if (!string.IsNullOrWhiteSpace(_token))
                getRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse())
                {
                    response.StatusCode = (int)getResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(getResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Get | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }

        public HttpResponseCustom Get(string url, string json)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(url);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/json; charset=utf-8";

            if (!string.IsNullOrWhiteSpace(_token))
                postRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                using (StreamWriter sWriter = new StreamWriter(postRequest.GetRequestStream()))
                {
                    sWriter.Write(json);
                }

                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse postResponse = (HttpWebResponse)postRequest.GetResponse())
                {
                    response.StatusCode = (int)postResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(postResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Get | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }

        public HttpResponseCustom Post(string url, string json)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(url);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/json; charset=utf-8";

            if (!string.IsNullOrWhiteSpace(_token))
                postRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                using (StreamWriter sWriter = new StreamWriter(postRequest.GetRequestStream()))
                {
                    sWriter.Write(json);
                }

                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse postResponse = (HttpWebResponse)postRequest.GetResponse())
                {
                    response.StatusCode = (int)postResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(postResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Post | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }
        public HttpResponseCustom Post(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(url);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/json; charset=utf-8";

            if (!string.IsNullOrWhiteSpace(_token))
                postRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                using (StreamWriter sWriter = new StreamWriter(postRequest.GetRequestStream()))
                {
                    //sWriter.Write(json);
                }

                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse postResponse = (HttpWebResponse)postRequest.GetResponse())
                {
                    response.StatusCode = (int)postResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(postResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Post | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }

        public HttpResponseCustom Put(string url, string json)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest putRequest = (HttpWebRequest)WebRequest.Create(url);
            putRequest.Method = "PUT";
            putRequest.ContentType = "application/json; charset=utf-8";

            if (!string.IsNullOrWhiteSpace(_token))
                putRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                using (StreamWriter sWriter = new StreamWriter(putRequest.GetRequestStream()))
                {
                    sWriter.Write(json);
                }

                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse putResponse = (HttpWebResponse)putRequest.GetResponse())
                {
                    response.StatusCode = (int)putResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(putResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Put | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }
        public HttpResponseCustom Delete(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            HttpWebRequest putRequest = (HttpWebRequest)WebRequest.Create(url);
            putRequest.Method = "DELETE";
            putRequest.ContentType = "application/json; charset=utf-8";

            if (!string.IsNullOrWhiteSpace(_token))
                putRequest.Headers.Add("Authorization", $"{_authType} {_token}");

            try
            {
                HttpResponseCustom response = new HttpResponseCustom();

                using (HttpWebResponse putResponse = (HttpWebResponse)putRequest.GetResponse())
                {
                    response.StatusCode = (int)putResponse.StatusCode;

                    using (StreamReader streamReader = new StreamReader(putResponse.GetResponseStream()))
                    {
                        response.Result = streamReader.ReadToEnd();
                    }
                }

                return response;
            }
            catch (WebException webEx)
            {
                var response = new HttpResponseCustom();

                if (webEx.Response == null)
                {
                    response.StatusCode = 500;
                    response.Result = webEx.Message;

                    return response;
                }

                using (HttpWebResponse e = (HttpWebResponse)webEx.Response)
                using (StreamReader streamReader = new StreamReader(e.GetResponseStream()))
                {
                    string responseString = streamReader.ReadToEnd();

                    if (e.StatusCode == HttpStatusCode.InternalServerError)
                        _logger.LogException(webEx, $"Delete | url: {url}, response: {responseString}");

                    response.StatusCode = (int)e.StatusCode;
                    response.Result = responseString;
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                var response = new HttpResponseCustom
                {
                    StatusCode = 500,
                    Result = JsonConvert.SerializeObject(new { Message = "An error has occured." })
                };

                return response;
            }
        }
    }
}