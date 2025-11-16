import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axios";
import AnswerSection from "../components/AnswerSection";
import { Card, Button, Modal } from "react-bootstrap";

export default function MainPage() {
  const [quotes, setQuotes] = useState([]);
  const [index, setIndex] = useState(0);
  const [showResult, setShowResult] = useState(false);
  const [isCorrect, setIsCorrect] = useState(null);
  const [attempts, setAttempts] = useState([]);
  const [showQuizModal, setShowQuizModal] = useState(false);
  const [correctCount, setCorrectCount] = useState(0);
  const [totalCount, setTotalCount] = useState(0);
  const navigate = useNavigate();

  useEffect(() => {
    (async () => {
      const r = await api.get("/quotes");
      setQuotes(r.data);
    })();
  }, []);

  if (!quotes.length) return <div>Loading...</div>;
  const current = quotes[index];

  const handleAnswer = (value) => {
    const correct = value === current.author;
    setIsCorrect(correct);
    setShowResult(true);
    setAttempts(a => [...a, {
      quoteId: current.id,
      selectedAnswer: value,
      isCorrect: correct,
      shownAt: new Date()
    }]);
  };

  const next = async () => {
    setShowResult(false);
    setIsCorrect(null);
    if (index + 1 < quotes.length) {
      setIndex(index + 1);
    } else {
      const token = localStorage.getItem("token");
      const userId = localStorage.getItem("userId");
      if (token && userId) {
        try {
          await api.post("/games", { userId, attempts });
          const count = attempts.filter(a => a.isCorrect).length;
          setCorrectCount(count);
          setTotalCount(attempts.length);
          setShowQuizModal(true);
        } catch (e) {
          console.error("Error saving game:", e);
        }
      } else {
        alert("Please login to save your game results.");
      }
      setIndex(0);
      setAttempts([]);
    }
  };

  return (
    <>
      <Card className="p-4 mt-3">
        <h3>Who Said It?</h3>
        <h4 className="mt-3">"{current.text}"</h4>

        <AnswerSection quote={current} onAnswer={handleAnswer} showResult={showResult} />

        {showResult && (
          <>
            {isCorrect ? (
              <p className="text-success"><b>Correct!</b> The right answer is: {current.author}</p>
            ) : (
              <p className="text-danger"><b>Sorry, you are wrong!</b> The right answer is: {current.author}</p>
            )}
            <Button onClick={next}>Next</Button>
          </>
        )}
      </Card>

      <Modal show={showQuizModal} onHide={() => setShowQuizModal(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title>Quiz Finished</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <h5>You got {correctCount} correct out of {totalCount} quiz!</h5>
          <p>Do you want to attempt another quiz?</p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={() => {
            setShowQuizModal(false);
            navigate("/quiz");
          }}>
            Yes
          </Button>
          <Button variant="secondary" onClick={() => {
            setShowQuizModal(false);
            navigate("/settings");
          }}>
            No
          </Button>
        </Modal.Footer>
      </Modal>

    </>
  );
}
