import React, { useState, useEffect } from "react";
import { FaCalendarPlus, FaEdit, FaTrash } from "react-icons/fa";

function App() {
  const [events, setEvents] = useState([]);
  const [prompt, setPrompt] = useState("");

  const fetchEvents = async () => {
    try {
      const response = await fetch("http://localhost:5173/api/orchestration/events");
      const data = await response.json();
      setEvents(data);
    } catch (error) {
      console.error("Error fetching events:", error);
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  const handleAdd = async () => {
    await fetch("http://localhost:5173/api/orchestration/process", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ prompt }),
    });
    setPrompt("");
    fetchEvents();
  };

  const handleUpdate = async (id) => {
    await fetch("http://localhost:5173/api/orchestration/update", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ id, title: "Updated Event" }),
    });
    fetchEvents();
  };

  const handleCancel = async (id) => {
    await fetch("http://localhost:5173/api/orchestration/cancel", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ id }),
    });
    fetchEvents();
  };

  return (
    <div style={styles.container}>
      <h1 style={styles.heading}>📅 AI Calendar</h1>

      <div style={styles.inputBox}>
        <input
          type="text"
          placeholder="Type event prompt (e.g. Math class at 10am)"
          value={prompt}
          onChange={(e) => setPrompt(e.target.value)}
          style={styles.input}
        />
        <button onClick={handleAdd} style={styles.addButton}>
          <FaCalendarPlus size={24} /> Add
        </button>
      </div>

      <div style={styles.eventsBox}>
        <h2 style={styles.subHeading}>Scheduled Events</h2>
        {events.length === 0 ? (
          <p style={styles.noEvents}>No events yet. Add one above!</p>
        ) : (
          <ul style={styles.list}>
            {events.map((evt, idx) => (
              <li key={idx} style={styles.eventItem}>
                <div style={{ flex: 1 }}>
                  <strong>{evt.title}</strong> <br />
                  <span>
                    {evt.start} → {evt.end}
                  </span>
                </div>
                <div>
                  <button onClick={() => handleUpdate(evt.id)} style={styles.updateButton}>
                    <FaEdit size={20} /> Update
                  </button>
                  <button onClick={() => handleCancel(evt.id)} style={styles.cancelButton}>
                    <FaTrash size={20} /> Cancel
                  </button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
}

const styles = {
  container: {
    fontFamily: "Arial, sans-serif",
    background: "linear-gradient(135deg, #9c27b0, #6a1b9a)",
    minHeight: "100vh",
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: "30px",
    color: "white",
  },
  heading: {
    fontSize: "3rem",
    marginBottom: "20px",
  },
  inputBox: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
    marginBottom: "30px",
  },
  input: {
    width: "500px",
    padding: "15px",
    borderRadius: "12px",
    border: "2px solid #ba68c8",
    fontSize: "1.2rem",
    marginRight: "15px",
  },
  addButton: {
    backgroundColor: "#8e24aa",
    color: "white",
    padding: "15px 25px",
    fontSize: "1.2rem",
    border: "none",
    borderRadius: "12px",
    cursor: "pointer",
    display: "flex",
    alignItems: "center",
    gap: "8px",
  },
  eventsBox: {
    backgroundColor: "white",
    borderRadius: "16px",
    padding: "25px",
    maxWidth: "700px",
    width: "100%",
    boxShadow: "0px 6px 15px rgba(0,0,0,0.2)",
    color: "#4a148c",
  },
  subHeading: {
    marginBottom: "15px",
    fontSize: "1.8rem",
    textAlign: "center",
  },
  list: {
    listStyleType: "none",
    padding: 0,
  },
  eventItem: {
    display: "flex",
    justifyContent: "space-between",
    alignItems: "center",
    background: "#f3e5f5",
    marginBottom: "15px",
    padding: "15px 20px",
    borderRadius: "12px",
    fontSize: "1.2rem",
  },
  updateButton: {
    backgroundColor: "#ab47bc",
    color: "white",
    border: "none",
    padding: "10px 15px",
    borderRadius: "8px",
    marginRight: "10px",
    cursor: "pointer",
    fontSize: "1rem",
    display: "flex",
    alignItems: "center",
    gap: "5px",
  },
  cancelButton: {
    backgroundColor: "#d32f2f",
    color: "white",
    border: "none",
    padding: "10px 15px",
    borderRadius: "8px",
    cursor: "pointer",
    fontSize: "1rem",
    display: "flex",
    alignItems: "center",
    gap: "5px",
  },
  noEvents: {
    textAlign: "center",
    fontSize: "1.2rem",
    color: "#7b1fa2",
  },
};

export default App;
