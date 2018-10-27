# Stage 1 - the build process
FROM node:carbon as builder
WORKDIR /usr/src/app
ARG STAGE
ENV STAGE=$STAGE
RUN npm install --global bower grunt
COPY package.json ./
RUN yarn install --production=false
COPY bower.json ./
RUN bower install --config.interactive=false --allow-root
COPY ./node-project ./node-project
WORKDIR /usr/src/app/node-project
RUN npm install
WORKDIR /usr/src/app
COPY . ./
RUN grunt build
CMD ["cp", "-TRpv", "/usr/src/app/dist/", "/dist/"]
