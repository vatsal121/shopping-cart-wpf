**** CREDENTIALS TO LOGIN ARE AT THE EOF ****

In our application, we have two users - Admin and Customer. 

They can login through username and password, after user has been authenticated, routing will take place and takes the customer to appropriate window based on their role.
- If customer logs in: 

- Customer can view products, add product to cart, remove product from cart, empty cart (remove all products), change product quantities in cart, view cart totals and check out (purchase all products in cart).
- On user side whenever the user adds a product to cart, only one qty of the product will be added to the cart by default.
- Upon adding the same product again its quantity will be updated in the cart rather than creating new entry.
- User can update any number of quantity from the cart page via data grid but quantity cannot be updated more than the availabe quantity.
- If a user sets the quantity as 0, the product will be automatically removed from the grid.

-If admin logs in: 

- On other hand, admin can view and update any product, view store inventory such as:
a) View all products in store inventory
b) Total units available (unsold) for each product
c) Total dollars in sales for the store
d) Add a new product to inventory
e) Increase the total units available for a product (order more stock) 
f) Update the products directly via the datagrid.

- There is common functionality for the admin and customer which is Change Password and Logout.

If user wants to change password, application provides that facility.

Finally, when a user is logged in, they can log out.

- New User Can SignUp.
1. New User Can signup via the Signup window from the login page.
	Validations in place:
		a). Username cannot be repeated.
		b). Password and confirm password should match.


P.S. If you want to update any value like Quantity on admin page, it can be directly edited/updated from the datagrid.
     UPDATE THE APP.CONFIG file of the project with the SQLServer Instance Name in order to connect to the DB.



** STEPS TO RESTORE DATABASE ** 

1. Open SQL Server Management Studio.
2. Open a new query window.
3. Open the .sql file named "ShoppingDBScript.sql" from the project folder.
4. Execute the script by pressing F5 or Execute button.
5. Upon executing it will create a database named "ShoppingDB_VMJP".
6. All the data will be inside the database as the script has both schema and data of the database.


**********************************
Testing credentials:
**********************************
No 	ID 	PASS	 Role
1) 	admin 	admin	 Admin
2) 	vatsal 	vatsal	 Customer
3) 	meet 	meet	 Customer
4) 	janki 	janki	 Customer
5) 	parth 	parth    Customer
**********************************

