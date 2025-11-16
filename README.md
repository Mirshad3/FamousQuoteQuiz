# FamousQuoteQuiz
Solution Description
1. Overview
This project is a full-stack implementation of the Famous Quote Quiz system. Users guess the authors
of famous quotes using two modes: Binary (Yes/No) and Multiple Choice. The system includes
authentication, administration, scoring, and session tracking.

2. Technologies Used
Frontend:
- React (Create React App)
- React Hooks
- Axios
- React-Bootstrap
- React Router
Backend:
- ASP.NET Core 8 Web API
- EF Core + SQL Server
- JWT Authentication
- BCrypt password hashing

3. Features Overview
Quiz:
- Binary & Multiple-choice modes
- Correct/wrong messages
- Author reveal
- Animated transitions
- Next question flow
- Automatic session saving when logged in
Admin API:
- User CRUD
- Quote CRUD
- Game session viewing

4. Database Structure
Tables:
- Users
- Quotes
- GameSessions
- QuestionAttempts
User ↔ Sessions ↔ Attempts ↔ Quotes

5. API Summary
Auth: login, register
Quotes: create, update, delete, list
Users: full management
Games: save session, history

7. Interactivity
- Disappearing answer buttons
- Bootstrap responsive UI
- Smooth transitions
- Animated success/error
- Auto-restart quiz



