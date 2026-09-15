#!/bin/bash
set -e

VM_USER="tim"
VM_HOST="192.168.50.199"
VM_DEPLOY_DIR="/opt/apps/TestAPI5"
PROJECT_DIR="/home/tim/Repos/TestAPI5/TestAPI5"

echo "Building..."
cd "$PROJECT_DIR"
dotnet publish -c Release -r linux-x64 --no-self-contained -o ./publish

echo "Ensuring ${VM_DEPLOY_DIR} exists on the VM..."
ssh -t ${VM_USER}@${VM_HOST} "sudo mkdir -p ${VM_DEPLOY_DIR} && sudo chown ${VM_USER}: ${VM_DEPLOY_DIR}"

echo "Copying to VM..."
rsync -avz --delete ./publish/ ${VM_USER}@${VM_HOST}:${VM_DEPLOY_DIR}/

echo "Done."
