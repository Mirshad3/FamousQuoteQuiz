import { useState, useEffect } from "react";
import { Card, Form } from "react-bootstrap";

export default function SettingsPage() {
  const [mode, setMode] = useState(
    localStorage.getItem("quizMode") || "binary"
  );

  useEffect(() => {
    localStorage.setItem("quizMode", mode);
  }, [mode]);

  return (
    <Card className="p-4 mt-3">
      <h3>Settings</h3>

      <Form.Check
        type="radio"
        label="Binary (Yes / No)"
        checked={mode === "binary"}
        onChange={() => setMode("binary")}
      />

      <Form.Check
        type="radio"
        label="Multiple Choice (3 options)"
        checked={mode === "multiple"}
        onChange={() => setMode("multiple")}
      />
    </Card>
  );
}
