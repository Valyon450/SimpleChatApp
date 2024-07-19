# SimpleChat Application

## Project overview

SimpleChatApp is a web application that allows users to engage in real-time messaging within different chats. This project is implemented using ASP.NET Core Web API for the backend. It leverages SignalR for real-time communication.

## Architecture

The application follows an n-tier client-server architecture. The Backend implemented using ASP.NET Core. Web API consists of data access tier, business logic and presentation tier. SignalR manages real-time messaging.

## Features

- Real-Time Messaging: Users can send, update, delete and receive messages in real-time.
- RESTful API: Backend API built on ASP.NET Core Web API, providing endpoints for chat management, message handling, and user operations with all validation.
- CRUD Operations: Full CRUD functionality for managing chats, messages, and users.
- Additional chat functionality: searching chats, joining a new user to the chat, lifting user from the chat, viewing all chat members and messages.

### Endpoints

#### Chats

- **GET /api/chat**: Retrieves all chats.
- **GET /api/chat/{id}**: Retrieves a specific chat by Id.
- **GET /api/chat/{id}/members**: Retrieves all members of the specific chat.
- **GET /api/chat/{id}/messages**: Retrieves all messages of the specific chat.
- **GET /api/chat/search**: Retrieves all chats with matches by the specific query.
- **POST /api/chat**: Creates a new chat.
- **PUT /api/chat**: Updates an existing chat.
- **DELETE /api/chat/{id}**: Deletes a chat by chat Id. In addition, need to specify the Id of the chat owner.
- **PATCH /api/chat/join**: Adds a user to the chat.
- **PATCH /api/chat/left**: Removes the user from the chat.

#### Messages

- **GET /api/message**: Retrieves all messages.
- **GET /api/message/{id}**: etrieves a specific message by Id.
- **POST /api/message**: Creates a new message.
- **PUT /api/message**: Updates an existing message.
- **DELETE /api/message/{id}**: Deletes a message by Id.

#### Users

- **GET /api/user**: Retrieves all users.
- **GET /api/user/{id}**: Retrieves a specific user by Id.
- **POST /api/user**: Creates a new user.
- **PUT /api/user**: Updates an existing user.
- **DELETE /api/user/{id}**: Deletes a user by Id.