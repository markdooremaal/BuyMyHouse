# Buy My House
Final assignment for the Cloud Databases Course

### To run this API you need the following services
- MySQL Database
- Azure storage container
  - Blob Storage
  - Queue
- Sendgrid account for sending e-mails

### How does the Mortgage calculation work?
1. The user will enter their income.
2. At midnight an Azure function will trigger. This calculates a fictional fixed-rate-mortgage and save it to the database.
3. After saving the system will insert the user id into the queue.
4. A queue trigger fires and sends the user an URL to view their mortgage offer generated by the API. The user can only view their offer for 24 hours.