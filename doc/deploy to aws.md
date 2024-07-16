## Frontend Deployment
React Ant Design frontend:
1. **AWS S3 (Static Website Hosting)**: Host your React application as a static website.
  - Build your React application using `npm run build`.
  - Upload the build output to an S3 bucket.
  - Configure the S3 bucket for static website hosting.
  - Use AWS CloudFront (optional) for CDN to enhance performance and provide HTTPS.

## Backend Deployment
C# .NET Core 8 backend:
1. **AWS Elastic Beanstalk**: This service can automatically handle the deployment, scaling, and load balancing of your .NET Core application.
  - Create an Elastic Beanstalk environment for your .NET Core application.
  - Use the Elastic Beanstalk CLI or the AWS Management Console to deploy your application.
  - Beanstalk will handle the provisioning of the underlying infrastructure (EC2 instances).

## Database Deployment
For your MSSQL database:
1. **Amazon RDS (Relational Database Service)**: Host your SQL Server database in a managed environment.
  - Create a new RDS instance with SQL Server.
  - Configure security groups to allow your Elastic Beanstalk environment to connect to the RDS instance.
  - Ensure you have proper backups and monitoring enabled.


Let's adjust the previous calculation considering the AWS Free Tier benefits for the first 12 months.
## Free Tier Limits:
1. **S3**:
    - 5 GB of Standard Storage
    - 20,000 GET requests
    - 2,000 PUT requests
    - 15 GB of data transfer out each month

2. **Elastic Beanstalk (EC2 Instances)**:
    - 750 hours of t2.micro or t3.micro instances each month

3. **RDS (MSSQL)**:
    - 750 hours of db.t3.micro instance each month
    - 20 GB of database storage
    - 20 GB of backup storage
  
## Cost Estimate with Free Tier:
**S3 (Static Website Hosting)**

- **Storage Cost**: 5 GB of free storage, only pay for extra.
  - Assume you use 10 GB: `10 GB - 5 GB (free) = 5 GB`
  - Storage cost: `5 GB * $0.023 = $0.115`
- **Data Transfer Out Cost**: 15 GB free per month
  - Assume you transfer 50 GB out per month: `50 GB - 15 GB (free) = 35 GB`
  - Data transfer cost: `35 GB * $0.09 = $3.15`

**Total S3 Cost**: $0.115 (storage) + $3.15 (data transfer) = **$3.27**

**Elastic Beanstalk (EC2 Instances)**

- **Instance Cost**: Free for 750 hours of t2.micro or t3.micro instances each month.
  - If you need more than 750 hours or larger instance types, additional costs apply.
  - Assume we need 750 hours of a `t3.micro` instance which is free.
  - No extra costs for EC2 instance.
- **Load Balancer Cost**: Load Balancers are not part of the free tier.
  - $0.0225 per hour: `$0.0225 per hour * 24 hours/day * 30 days = $16.20`
  - Data processing cost: Assume 100 GB processed per month: `100 GB * $0.008 = $0.80`
 
**Total Elastic Beanstalk Cost**: $16.20 (load balancer) + $0.80 (data processing) = **$17.00**

**RDS (MSSQL)**

- **Instance Cost**: Free for 750 hours of db.t3.micro instance each month.
  - Assume you need exactly 750 hours which is free.
  - No extra costs for the instance.
- **Storage Cost**: Free for 20 GB of database storage.
  - Assume 100 GB storage: `100 GB - 20 GB (free) = 80 GB`
  - Storage cost: `80 GB * $0.115 = $9.20`
- **Backup Storage Cost**: Free for 20 GB of backup storage.

**Total RDS Cost**: $9.20 (storage)

## Total Estimated Monthly Cost with Free Tier
- **S3**: $3.27
- **Elastic Beanstalk**: $17.00
- **RDS**: $9.20
  
**Total Cost**: $3.27 (S3) + $17.00 (Elastic Beanstalk) + $9.20 (RDS) = **$29.47**

## Notes:
1. **Eligibility**: This calculation assumes you are eligible for the AWS Free Tier benefits.
2. **Usage Patterns**: Free Tier benefits reset every month; exceeding these limits will incur additional charges.
3. **Services**: Only certain services and instance types are covered by the Free Tier.
4. **Free Tier Duration**: The Free Tier benefits are only available for the first 12 months from your AWS account creation date.
