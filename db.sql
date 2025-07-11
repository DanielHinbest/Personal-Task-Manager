-- Personal Task Manager Sample Data Script
-- SQLite Database Schema and Sample Data

-- Clear existing data (if any)
DELETE FROM Tasks;
DELETE FROM Categories;

-- Reset auto-increment counters
DELETE FROM sqlite_sequence WHERE name IN ('Categories', 'Tasks');

-- Insert Categories
INSERT INTO Categories (Name, CreatedAt) VALUES 
('Work', '2024-01-15 09:00:00'),
('Personal', '2024-01-15 09:15:00'),
('Health & Fitness', '2024-01-16 10:30:00'),
('Home & Garden', '2024-01-17 14:20:00'),
('Learning', '2024-01-18 08:45:00'),
('Finance', '2024-01-19 16:30:00'),
('Shopping', '2024-01-20 11:00:00'),
('Travel', '2024-01-21 13:15:00');

-- Insert Tasks
INSERT INTO Tasks (Title, Description, Priority, Status, DueDate, CreatedAt, UpdatedAt, CategoryId) VALUES 
-- Work Tasks
('Complete Project Proposal', 'Finish the Q1 project proposal and submit to management for review', 'High', 'In Progress', '2024-07-15 17:00:00', '2024-07-10 09:00:00', '2024-07-12 14:30:00', 1),
('Team Meeting Preparation', 'Prepare agenda and materials for weekly team standup meeting', 'Medium', 'Pending', '2024-07-14 10:00:00', '2024-07-11 16:20:00', '2024-07-11 16:20:00', 1),
('Code Review', 'Review pull requests from junior developers and provide feedback', 'Medium', 'Completed', '2024-07-12 15:00:00', '2024-07-09 11:15:00', '2024-07-12 14:45:00', 1),
('Client Presentation', 'Prepare slides and demo for client presentation next week', 'High', 'Pending', '2024-07-18 14:00:00', '2024-07-11 13:30:00', '2024-07-11 13:30:00', 1),
('Update Documentation', 'Update API documentation with recent changes and new endpoints', 'Low', 'Pending', '2024-07-20 16:00:00', '2024-07-10 10:45:00', '2024-07-10 10:45:00', 1),
('Security Audit', 'Conduct security review of the authentication system', 'High', 'In Progress', '2024-07-16 12:00:00', '2024-07-08 14:20:00', '2024-07-12 09:15:00', 1),

-- Personal Tasks
('Dentist Appointment', 'Annual dental checkup and cleaning appointment', 'Medium', 'Pending', '2024-07-22 14:30:00', '2024-07-05 12:00:00', '2024-07-05 12:00:00', 2),
('Birthday Party Planning', 'Plan surprise birthday party for Sarah - venue, catering, decorations', 'High', 'In Progress', '2024-07-25 18:00:00', '2024-07-08 19:30:00', '2024-07-11 20:15:00', 2),
('Car Insurance Renewal', 'Review and renew car insurance policy before expiration', 'Medium', 'Pending', '2024-07-28 23:59:00', '2024-07-10 08:45:00', '2024-07-10 08:45:00', 2),
('Photo Organization', 'Organize and backup family photos from the last 6 months', 'Low', 'Pending', '2024-07-30 20:00:00', '2024-07-09 15:20:00', '2024-07-09 15:20:00', 2),
('Call Mom', 'Weekly check-in call with mom to catch up', 'Medium', 'Completed', '2024-07-13 19:00:00', '2024-07-13 10:30:00', '2024-07-13 19:30:00', 2),

-- Health & Fitness Tasks
('Morning Jog', 'Daily 30-minute jog around the neighborhood park', 'Medium', 'Completed', '2024-07-13 07:00:00', '2024-07-13 06:30:00', '2024-07-13 07:35:00', 3),
('Gym Session', 'Full body workout - strength training and cardio', 'Medium', 'Pending', '2024-07-14 18:00:00', '2024-07-12 20:15:00', '2024-07-12 20:15:00', 3),
('Meal Prep Sunday', 'Prepare healthy meals for the upcoming week', 'High', 'Pending', '2024-07-14 15:00:00', '2024-07-11 11:40:00', '2024-07-11 11:40:00', 3),
('Doctor Appointment', 'Annual physical examination and health screening', 'High', 'Pending', '2024-07-19 10:00:00', '2024-07-02 14:25:00', '2024-07-02 14:25:00', 3),
('Yoga Class', 'Evening yoga class for flexibility and stress relief', 'Low', 'In Progress', '2024-07-15 19:30:00', '2024-07-12 16:45:00', '2024-07-12 16:45:00', 3),

-- Home & Garden Tasks
('Lawn Mowing', 'Weekly lawn maintenance and edge trimming', 'Medium', 'Pending', '2024-07-14 10:00:00', '2024-07-12 18:30:00', '2024-07-12 18:30:00', 4),
('Kitchen Deep Clean', 'Deep clean kitchen including appliances and cabinets', 'High', 'Pending', '2024-07-16 12:00:00', '2024-07-11 09:15:00', '2024-07-11 09:15:00', 4),
('Plant Watering', 'Water all indoor and outdoor plants', 'Low', 'Completed', '2024-07-13 08:00:00', '2024-07-13 07:45:00', '2024-07-13 08:15:00', 4),
('Garage Organization', 'Organize garage storage and dispose of unused items', 'Medium', 'In Progress', '2024-07-21 16:00:00', '2024-07-10 13:20:00', '2024-07-12 15:30:00', 4),
('Fix Leaky Faucet', 'Repair the dripping bathroom faucet', 'High', 'Pending', '2024-07-17 14:00:00', '2024-07-09 20:10:00', '2024-07-09 20:10:00', 4),

-- Learning Tasks
('JavaScript Course', 'Complete Module 5 of the advanced JavaScript online course', 'Medium', 'In Progress', '2024-07-18 22:00:00', '2024-07-08 21:30:00', '2024-07-12 19:45:00', 5),
('Read Programming Book', 'Finish reading "Clean Code" by Robert Martin', 'Low', 'Pending', '2024-07-25 23:59:00', '2024-07-01 16:00:00', '2024-07-01 16:00:00', 5),
('Language Practice', 'Daily 30-minute Spanish practice using language app', 'Medium', 'Pending', '2024-07-14 20:00:00', '2024-07-11 07:30:00', '2024-07-11 07:30:00', 5),
('Online Workshop', 'Attend UX Design workshop - "Design Thinking Fundamentals"', 'High', 'Pending', '2024-07-16 13:00:00', '2024-07-05 11:45:00', '2024-07-05 11:45:00', 5),

-- Finance Tasks
('Monthly Budget Review', 'Review and analyze monthly expenses and adjust budget', 'High', 'Pending', '2024-07-15 20:00:00', '2024-07-10 17:30:00', '2024-07-10 17:30:00', 6),
('Investment Portfolio', 'Review investment portfolio performance and rebalance if needed', 'Medium', 'Pending', '2024-07-20 18:00:00', '2024-07-08 12:15:00', '2024-07-08 12:15:00', 6),
('Tax Document Filing', 'Organize and file tax documents in preparation for next year', 'Low', 'Pending', '2024-07-31 17:00:00', '2024-07-06 14:40:00', '2024-07-06 14:40:00', 6),
('Emergency Fund Check', 'Review emergency fund amount and add monthly contribution', 'Medium', 'Completed', '2024-07-12 16:00:00', '2024-07-12 10:20:00', '2024-07-12 16:30:00', 6),

-- Shopping Tasks
('Grocery Shopping', 'Weekly grocery shopping for meal prep and household items', 'High', 'Pending', '2024-07-14 11:00:00', '2024-07-13 21:15:00', '2024-07-13 21:15:00', 7),
('New Running Shoes', 'Buy new running shoes to replace worn-out pair', 'Medium', 'Pending', '2024-07-17 19:00:00', '2024-07-09 18:25:00', '2024-07-09 18:25:00', 7),
('Gift for Anniversary', 'Find and purchase anniversary gift for spouse', 'High', 'In Progress', '2024-07-23 17:00:00', '2024-07-07 13:50:00', '2024-07-11 16:20:00', 7),
('Office Supplies', 'Purchase office supplies: notebooks, pens, and organizers', 'Low', 'Pending', '2024-07-22 15:00:00', '2024-07-10 12:35:00', '2024-07-10 12:35:00', 7),

-- Travel Tasks
('Vacation Planning', 'Research and book summer vacation destination and accommodations', 'High', 'In Progress', '2024-07-20 23:59:00', '2024-07-01 10:00:00', '2024-07-11 14:20:00', 8),
('Passport Renewal', 'Renew passport before international trip next month', 'High', 'Pending', '2024-07-18 16:00:00', '2024-07-03 09:30:00', '2024-07-03 09:30:00', 8),
('Pack Suitcase', 'Pack clothes and essentials for weekend trip', 'Medium', 'Pending', '2024-07-19 08:00:00', '2024-07-12 22:10:00', '2024-07-12 22:10:00', 8),
('Travel Insurance', 'Purchase travel insurance for upcoming European trip', 'Medium', 'Completed', '2024-07-10 17:00:00', '2024-07-09 11:25:00', '2024-07-10 17:45:00', 8);

-- Verify the data was inserted correctly
SELECT 'Categories Count: ' || COUNT(*) as Summary FROM Categories
UNION ALL
SELECT 'Tasks Count: ' || COUNT(*) FROM Tasks
UNION ALL
SELECT 'High Priority Tasks: ' || COUNT(*) FROM Tasks WHERE Priority = 'High'
UNION ALL
SELECT 'Medium Priority Tasks: ' || COUNT(*) FROM Tasks WHERE Priority = 'Medium'  
UNION ALL
SELECT 'Low Priority Tasks: ' || COUNT(*) FROM Tasks WHERE Priority = 'Low'
UNION ALL
SELECT 'Completed Tasks: ' || COUNT(*) FROM Tasks WHERE Status = 'Completed'
UNION ALL
SELECT 'In Progress Tasks: ' || COUNT(*) FROM Tasks WHERE Status = 'In Progress'
UNION ALL
SELECT 'Pending Tasks: ' || COUNT(*) FROM Tasks WHERE Status = 'Pending';

-- Show sample of data by category
SELECT 
    c.Name as Category,
    COUNT(t.Id) as TaskCount,
    SUM(CASE WHEN t.Status = 'Completed' THEN 1 ELSE 0 END) as Completed,
    SUM(CASE WHEN t.Status = 'In Progress' THEN 1 ELSE 0 END) as InProgress,
    SUM(CASE WHEN t.Status = 'Pending' THEN 1 ELSE 0 END) as Pending
FROM Categories c
LEFT JOIN Tasks t ON c.Id = t.CategoryId
GROUP BY c.Id, c.Name
ORDER BY c.Name;