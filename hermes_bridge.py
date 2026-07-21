import http.server
import socketserver
import urllib.parse
import sys

# Mesaj depolama
messages = []

class HermesBridge(http.server.SimpleHTTPRequestHandler):
    def do_POST(self):
        content_length = int(self.headers['Content-Length'])
        post_data = self.rfile.read(content_length).decode('utf-8')
        params = urllib.parse.parse_qs(post_data)
        msg = params.get('msg', [''])[0]
        
        print(f"\n[UNITY -> HERMES]: {msg}")
        sys.stdout.flush()
        
        # Bu mesajı konsola veya bir dosyaya yazabiliriz
        with open("C:/Users/student/Desktop/BozkurtIlker/Unity/Brave/messages.txt", "a", encoding="utf-8") as f:
            f.write(f"USER: {msg}\n")
            
        messages.append(msg)
        
        self.send_response(200)
        self.end_headers()
        self.wfile.write(b"OK")

    def do_GET(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/plain; charset=utf-8')
        self.end_headers()
        
        # Dosyadan yanıt var mı kontrol et
        try:
            with open("C:/Users/student/Desktop/BozkurtIlker/Unity/Brave/reply.txt", "r", encoding="utf-8") as f:
                reply = f.read().strip()
            if reply:
                self.wfile.write(reply.encode('utf-8'))
                # Gönderildikten sonra dosyayı temizle
                with open("C:/Users/student/Desktop/BozkurtIlker/Unity/Brave/reply.txt", "w", encoding="utf-8") as f:
                    f.write("")
                return
        except FileNotFoundError:
            pass
            
        self.wfile.write(b"WAIT")

if __name__ == "__main__":
    # Portu bırakmak için
    socketserver.TCPServer.allow_reuse_address = True
    try:
        with socketserver.TCPServer(("", 8080), HermesBridge) as httpd:
            print("Hermes Bridge çalışıyor: http://localhost:8080")
            sys.stdout.flush()
            httpd.serve_forever()
    except Exception as e:
        print(f"HATA OLUŞTU: {e}")
        sys.stdout.flush()
