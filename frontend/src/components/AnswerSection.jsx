import { Button } from "react-bootstrap";

export default function AnswerSection({ quote, onAnswer, showResult }) {
  const mode = localStorage.getItem("quizMode") || "binary";
  if (showResult) return null;
  if (mode === "binary") {
    return (
      <div className="mt-3">
        <p>Is the author <b>{quote.author}</b>?</p>
        <Button className="me-2" onClick={() => onAnswer(quote.author)}>Yes</Button>
        <Button variant="secondary" onClick={() => onAnswer("No")}>No</Button>
      </div>
    );
  }
  return (
    <div className="mt-3">
      {[quote.optionA, quote.optionB, quote.optionC].map(opt => (
        <Button key={opt} className="d-block mb-2" onClick={() => onAnswer(opt)}>{opt}</Button>
      ))}
    </div>
  );
}
