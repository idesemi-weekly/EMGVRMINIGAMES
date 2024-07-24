import socket

def udp_client():
    server_address = ('localhost', 11000)
    message = '{"type":"emg", "data":[0.99,0.99,0.023273082,0.04411545,0.8]}'

    # Créer un socket UDP
    with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as sock:
        try:
            # Envoyer des données
            print(f"Envoi de {message} à {server_address}")
            sent = sock.sendto(message.encode(), server_address)

        except Exception as e:
            print(f"Erreur : {e}")

if __name__ == "__main__":
    udp_client()