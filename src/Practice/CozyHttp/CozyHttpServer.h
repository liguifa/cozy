#ifndef __COZY__HTTP_SERVER__
#define __COZY__HTTP_SERVER__

#include "uv.h"
#include <string>

class CozyConnection;

class CozyHttpServer
{
    typedef void(*work_cb)(CozyConnection* conn);

public:

    CozyHttpServer();
    ~CozyHttpServer();

    void Init(const std::string& ip, int port, int maxConn);
    void Start();
    void Stop();

    void SetCallback(work_cb cb);

protected:
    static void _OnConnect(uv_stream_t* handle, int status);
    static void _OnRead(uv_stream_t * handle, ssize_t nread, const uv_buf_t* buf);
    static void _OnWrite(uv_write_t* req, int status);
    static void _OnClose(uv_handle_t* handle);

    static void alloc_buffer(uv_handle_t* handle, size_t suggested_size, uv_buf_t* buf);

private:
    uv_loop_t*      m_loop;
    uv_tcp_t        m_server;
    int             m_maxConn;
    sockaddr_in     m_addr;
    work_cb         m_work_cb;
};

#endif // __COZY__HTTP_SERVER__