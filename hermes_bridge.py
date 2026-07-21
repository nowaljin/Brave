import http.server
import socketserver
import urllib.parse
import sys
import threading

# Mesajları ve yanıtları tutan paylaşımlı hafıza
class HermesState:
    last_msg = ""
    last_reply = "WAIT"

state = HermesState()

class HermesBridge(http.server.SimpleHTTPRequestHandler):
    def do_POST(self):
        content_length = int(self.headers['Content-Length'])
        post_data = self.rfile.read(content_length).decode('utf-8')
        params = urllib.parse.parse_qs(post_data)
        msg = params.get('msg', [''])[0]
        
        print(f"\n[UNITY -> HERMES]: {msg}")
        sys.stdout.flush()
        state.last_msg = msg
        
        self.send_response(200)
        self.end_headers()
        self.wfile.write(b"OK")

    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/plain; charset=utf-8')
        self.end_headers()
        
        # Hafızadaki yanıtı gönder
        self.wfile.write(state.last_reply.encode('utf-8'))
        # Gönderdikten sonra yanıtı sıfırla
        state.last_reply = "WAIT"

def run_server():
    socketserver.TCPServer.allow_reuse_address = True
    with socketserver.TCPServer(("", 8080), HermesBridge) as httpd:
        print("Hermes Bridge çalışıyor: http://localhost:8080")
        sys.stdout.flush()
        httpd.serve_forever()

if __name__ == "__main__":
    threading.Thread(target=run_server, daemon=True).start()
    
    # Konsoldan sana yanıt yazabilmen için bir döngü
    while True:
        reply = input("Hermes Yanıtı: ")
        state.last_reply = reply
