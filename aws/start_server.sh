#!/bin/bash
source ~/.bashrc

cd /home/ec2-user/

mkdir -p qa
rm -rf ./qa/*
cp -r code/* qa
rm -rf code/*
cd qa

tar -zxf server.tar.gz
pm2 stop --silent qa
pm2 delete --silent qa
pm2 start ecosystem.config.js --only qa

pm2 save

at -M now + 2 minute <<< $'service codedeploy-agent restart'

