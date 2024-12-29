#!/bin/bash

# Create the SSL config file for Nginx
cat <<EOF > /etc/nginx/conf.d/https.conf
server {
    listen 80;
    listen 443 ssl;
    server_name pointlessapi.com;

    ssl_certificate /etc/letsencrypt/live/pointlessapi.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/pointlessapi.com/privkey.pem;

    location / {
        proxy_pass http://127.0.0.1:5000;
        proxy_set_header Host \$host;
        proxy_set_header X-Real-IP \$remote_addr;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto https;
    }
}
EOF

# Reload Nginx to apply the changes
service nginx reload
