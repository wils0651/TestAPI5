#!/bin/bash
set -e

PI_USER="pi"
PI_HOST="192.168.50.3"
PI_DEPLOY_DIR="/home/pi/NetDeploy/TestAPI5/net10.0/linux-arm64"
SERVICE_NAME="testAPI5.service"
PROJECT_DIR="/home/tim/Repos/TestAPI5/TestAPI5"

echo "Building..."
cd "$PROJECT_DIR"
dotnet publish -c Release -r linux-arm64 --no-self-contained -o ./publish

echo "Copying to Pi..."
rsync -avz --delete ./publish/ ${PI_USER}@${PI_HOST}:${PI_DEPLOY_DIR}/

echo "Restarting service..."
ssh ${PI_USER}@${PI_HOST} "sudo systemctl restart ${SERVICE_NAME}"

echo "Done."
