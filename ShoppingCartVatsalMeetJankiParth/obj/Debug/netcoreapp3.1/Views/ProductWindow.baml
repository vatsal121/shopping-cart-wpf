   M S B A M L   `   `   `  ����[  WShoppingCartVatsalMeetJankiParth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views&ShoppingCartVatsalMeetJankiParth.Views  5   	   :    4ShoppingCartVatsalMeetJankiParth.Views.ProductWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views     5   	     � Title$  Shopping Window��5   	    � SizeToContent$ WidthAndHeight=�6!    � WindowStartupLocation$ CenterScreen=�6@    � 	Resources 5	       �� 
TargetType �� Property �� Value��%t  '��       '�� �     '�� �     �� 5
   
   " ��6   .�5      ��  � ��$ 10��6&   �� 5       � ��$ 10��6'   �� 5       �@J�Opacity$ 0Q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ 10, 5q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   �� 5       � ��$ Center=�62   5      5      .��5      �� $	��678��6   $	��500��6   .+�5   
   �� �� 5   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5      �� -   5         ��Name$ UserNameLabel��5      $
��Bold"�6+   6?   5      �� 5   
   $��0,20,5,0q�6   $ 
Horizontal=�6'   $��Right=�6@   .+�5      �� -   5        	 ��Name$	 ChangePasswordButton��5      $��Change Password��5      ��5      �� 
 �� Path$
 ChangePasswordCommand��� 5!      �� 5"      -   5        $	 LogoutButton��5"      $��Logout��5#      ��5$      �� $
 LogOutCommand�5%      �� 5&   
   $��10q�6   �� 5'   
   .+�5(      &   System.Windows.Controls.DataGrid  -   5          Name$ CustomerProductDataGrid��5(         AutoGenerateColumns$ False��5)      $	��300��5*      $	��Top=�5-      $	��663��6(   ��5+      �� $
 ProductList� �� SelectedItem 5,      �� $
 SelectedProduct�   Columns 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6)      MinWidth$ 80Q�52   %      
IsReadOnly$
 True��53   )   1  +System.Windows.Controls.DataGridBoundColumn   Binding 54   %   �� $
 ProductName� �� Mode$ OneWay=�  55      $ Category��6)   $ 80Q�56   %   $
 True��57   *    58   %   �� $
 Category.CategoryName�$ OneWay=�  59      $ Price��6)   $ 80Q�5:   )   $
 True��5;   )    5<   )   �� $
 Price�$ OneWay=�  5=      $ Description��6)   $ 80Q�5>   %   $
 True��5?   *    5@   %   �� $
 Description�$ OneWay=�  5B      $ Quantity��6)   $ 80Q�5C   )   $
 True��5D   )    5E   )   �� $
 Quantity�$ OneWay=�5P      5Q      �� 5R   
   �� 5S   
   $ 
Horizontal=�6   .+�5U      �� -   5        $	 AddCartButton��5U      $��Add to cart��5V      ��5W      �� $
 AddCartCommand��� 5X      �� 5Z      -   5        $	 ViewCartButton��5Z      $��	View cart��5[      ��5\      �� $
 ViewCartCommand�5^      5_      5`      