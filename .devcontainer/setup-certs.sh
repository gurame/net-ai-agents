#!/bin/sh
set -e

sudo cp /https/localhost.crt /usr/local/share/ca-certificates/localhost.crt
sudo update-ca-certificates
