import { useState } from "react";
import api from "../api/axios";
import { Form, Button, Card } from "react-bootstrap";

export default function Register() {
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [err, setErr] = useState("");

  const submit = async (e) => {
    e.preventDefault();
    try {
      const r = await api.post("/auth/register", { fullName, email, password });
      localStorage.setItem("token", r.data.token);
      localStorage.setItem("userId", r.data.user.id);
      window.location.href = "/";
    } catch (ex) {
      setErr(ex.response?.data ?? "Registration failed");
    }
  };

  return (
    <Card className="p-4 mt-3">
      <h3>Register</h3>
      <Form onSubmit={submit}>
        {err && <div className="text-danger">{err}</div>}
        <Form.Group className="mb-2"><Form.Label>Full name</Form.Label><Form.Control value={fullName} onChange={e => setFullName(e.target.value)} /></Form.Group>
        <Form.Group className="mb-2"><Form.Label>Email</Form.Label><Form.Control value={email} onChange={e => setEmail(e.target.value)} /></Form.Group>
        <Form.Group className="mb-2"><Form.Label>Password</Form.Label><Form.Control type="password" value={password} onChange={e => setPassword(e.target.value)} /></Form.Group>
        <Button type="submit">Register</Button>
      </Form>
    </Card>
  );
}
