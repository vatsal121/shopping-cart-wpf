   M S B A M L   `   `   `  ����[  WShoppingCartVatsalMeetJankiParth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views&ShoppingCartVatsalMeetJankiParth.Views  5   	   8    2ShoppingCartVatsalMeetJankiParth.Views.AdminWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views     5   	     � Title$  Admin Window��5   	    � SizeToContent$ WidthAndHeight=�5	   	    � WindowStartupLocation$ CenterScreen=�6(   $	��700��6M    � 	Resources 5       �� 
TargetType �� Property �� Value��%d  '��       '�� H     '�� �     '�� *    '�� �    �� 5   
   " ��6   .�5      ��  � ��$ 80��6&   5      �� 5   
   " ��6   .�5      ��  � ��$ 10��6&   �� 5       � ��$ 10��6'   �� 5       �@J�Opacity$ 0Q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ 10, 5q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   �� 5       � ��$ Center=�62   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�69   �� 5       � ��$ 80��6)   �� 5       � ��$ 80��6)   5      5       .��5!      �� $
��Auto��6   $	��650��6   .+�5"   
   �� �� 5#   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5$      �� -   5         ��Name$ UserNameLabel��5$      $
��Bold"�6+   $	��200��6=   6K   5%      �� 5&   
   $��0,20,5,0q�6   $ 
Horizontal=�6'   $��Right=�6@   .+�5'      �� -   5        	 ��Name$	 ChangePasswordButton��5'      $��Change Password��5(      ��5)      �� 
 �� Path$
 ChangePasswordCommand��� 5*      �� 5+      -   5        $	 LogoutButton��5+      $��Logout��5,      ��5-      �� $
 LogOutCommand�5.      �� 5/   
   �� 50   
   -   5        $��TotalSalesLabel��50      $
��Left=�6�   $	��286��6�   ��6'   �� $
 
TotalSales� �� FallbackValue$ 
TotalSales�� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 51   
   �� 52   
   &   System.Windows.Controls.DataGrid  53   
   -   5          Name$ CustomerProductDataGrid��53         AutoGenerateColumns$ False��54      $	��300��55      $	��Top=�58      $	��882��6'   ��56      �� $
 ProductList� �� SelectedItem 57      �� $
 SelectedProduct�   Columns 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      MinWidth$ 80Q�5<   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5=   %   �� $
 ProductName�  5>      $ Category��6%   $ 80Q�5?   %      
IsReadOnly$
 True��5@   %    5A   %   �� $
 Category.CategoryName�  5B      $ Price��6%   $ 80Q�5C   )    5D   )   �� $
 Price�  5E      $ Description��6%   $ 80Q�5F   %    5G   %   �� $
 Description�  5I      $ Quantity��6%   $ 80Q�5J   )    5K   )   �� $
 Quantity�  5L      $ Date Created��6%   $ 80Q�5M   %   $
 True��5N   %    5O   %   �� $
 DateCreated�  5Q      $ Date Modified��6%   $ 80Q�5R   )   $
 True��5S   )    5T   )   �� $
 DateModified�5W      �� 5X   
   �� 5Y   
   �� 5Z   
   �� 5[   
   $ 
Horizontal=�6   $��Center=�6.   .+�5\      �� $ 
Horizontal=�6   .+�5]      �� $
��Name��6   �� 5^      ��6   �� $
 ProductName�$ PropertyChanged=�5_      �� 5`      �� 5a      $ 
Horizontal=�6   .+�5b      �� $��Category��6   �� 5c      -   5         ��Name$ CategoryDropDown��5c      $��80��65    �� DisplayMemberPath$ CategoryName��6j    �� SelectedValuePath$ Id��5d      ��5c   @   �� $
 CategoryList� 5e      �� $
 SelectedCategory�$ PropertyChanged=� �� SelectedValue 5f      �� $
 SelectedCategory.Id�5g      �� 5h      �� 5i      $ 
Horizontal=�6   .+�5j      �� $��Description��6   �� 5k      ��6   �� $
 Description�$ PropertyChanged=�5l      �� 5m      �� 5n      $ 
Horizontal=�6   .+�5o      �� $��Price��6   �� 5p      ��6   �� $
 Price�$ PropertyChanged=�5q      �� 5r      �� 5s      $ 
Horizontal=�6   .+�5t      �� $��Quantity��6   �� 5u      ��6   �� $
 Quantity�$ PropertyChanged=�5v      �� 5w      5x      �� 5y   
   �� 5z   
   $ 
Horizontal=�6   .+�5{      �� -   5        $	 AddProductButton��5{      $��Add product��5|      ��5}      �� $
 AddNewProductCommand���5~      ��  �� ElementName$ CategoryDropDown���� 5�      �� 5�      -   5        $	 DeleteProductButton��5�      $��Delete Product��5�      ��5�      �� $
 RemoveProductCommand�5�      5�      5�      