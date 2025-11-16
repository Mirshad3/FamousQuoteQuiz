import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import Login from "./pages/Login";
import Register from "./pages/Register";
import MainPage from "./pages/MainPage";
import SettingsPage from "./pages/SettingsPage";
import { Container, Nav } from "react-bootstrap";

function App() {
  const token = localStorage.getItem("token");
  return (
    <BrowserRouter>
      <Container className="mt-3">
        <Nav variant="tabs">
          <Nav.Item><Nav.Link as={Link} to="/">Quiz</Nav.Link></Nav.Item>
          <Nav.Item><Nav.Link as={Link} to="/settings">Settings</Nav.Link></Nav.Item>
          {!token ? (
            <>
              <Nav.Item><Nav.Link as={Link} to="/login">Login</Nav.Link></Nav.Item>
              <Nav.Item><Nav.Link as={Link} to="/register">Register</Nav.Link></Nav.Item>
            </>
          ) : (
            <Nav.Item><Nav.Link onClick={() => { localStorage.removeItem("token"); window.location.href = "/"; }}>Logout</Nav.Link></Nav.Item>
          )}
        </Nav>

        <Routes>
          <Route path="/" element={<MainPage />} />
          <Route path="/settings" element={<SettingsPage />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Routes>
      </Container>
    </BrowserRouter>
  );
}

export default App;
