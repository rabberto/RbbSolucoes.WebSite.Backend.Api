// MongoDB Initialization Script
db = db.getSiblingDB('rbbsolucoes');

// Create collections if they don't exist
db.createCollection('About');
db.createCollection('Contact');
db.createCollection('ContactMessage');
db.createCollection('Service');
db.createCollection('Technology');

// Create indexes for better performance
db.ContactMessage.createIndex({ "createdAt": 1 });
db.ContactMessage.createIndex({ "status": 1 });
db.Service.createIndex({ "isActive": 1 });
db.Technology.createIndex({ "isActive": 1 });

print('Database initialized successfully!');