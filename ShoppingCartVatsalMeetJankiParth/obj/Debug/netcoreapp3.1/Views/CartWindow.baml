   M S B A M L   `   `   `  ����[  WShoppingCartVatsalMeetJankiParth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views&ShoppingCartVatsalMeetJankiParth.Views  5   	   7    1ShoppingCartVatsalMeetJankiParth.Views.CartWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:ShoppingCartVatsalMeetJankiParth.Views     5   	     � Title$  Cart Window��5   	   $	��384��6   $	��654��6*    � WindowStartupLocation$ CenterScreen=�66    � 	Resources 5	       �� 
TargetType �� Property �� Value��%t  '��       '�� �     '�� �     �� 5
   
   " ��6   .�5      ��  � ��$ 10��6&   �� 5       � ��$ 10��6'   �� 5       �@J�Opacity$ 0Q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ 10, 5q�6(   5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   �� 5       � ��$ Center=�62   5      5      .��5      �� $	��609��6   $	��317��6   .+�5   
   �� $	��0,0q�6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   .+�5      �� -   5         ��Name$ 
BackButton��5      $
��Back��5      ��5      ��  �� Path$ GoBackCommand�5      �� 5   
   $��10q�6   &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 CustomerProductDataGrid��5      $��Stretch=�5       
   AutoGenerateColumns$
 False��5!      $	��150��5"      $	��400��6#   $	��Top=�5%      $	��588��6(   ��5#      �� $ CartList� �� SelectedItem 5$      �� $ SelectedProduct�   Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      MinWidth$ 80Q�5)   %      
IsReadOnly$
 True��5*   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5+   %   �� $  ProductDetails.ProductName� �� Mode$ OneWay=�  5,      $ Category��6%   $ 80Q�5-   %   $
 True��5.   %    5/   %   �� $* $ProductDetails.Category.CategoryName�$ OneWay=�  51      $ Description��6%   $ 80Q�52   %   $
 True��53   %    54   %   �� $  ProductDetails.Description�$ OneWay=�  56      $ Quantity��6%   $ 80Q�57   )    58   )   �� $ 
QtyOrdered�  5:      $ Original Price��6%   $ 80Q�5;   )   $
 True��5<   )    5=   )   �� $ ProductDetails.Price�  5?      $ Discount Percentage��6%   $ 80Q�5@   )   $
 True��5A   )    5B   )   �� $ DiscountPercentage�  5E      $ Final Price��6%   $ 80Q�5F   )   $
 True��5G   )    5H   )   �� $ FinalProductPrice�5L      �� 5M   
   �� 5N   
   $ 
Horizontal=�6   .+�5O      �� ��6   �� $ 	CartTotal� �� FallbackValue$ 	CartTotal�� �� UpdateSourceTrigger$ PropertyChanged=�5R      �� 5S   
   �� 5T   
   $ 
Horizontal=�6   .+�5V      �� -   5        $ RemoveCartButton��5V      $��Remove from cart��5W      ��5X      �� $ RemoveCartCommand��� 5Y      �� 5Z      -   5        $ EmptyCartButton��5Z      $��
Empty cart��5[      ��5\      �� $ EmptyCartCommand��� 5]      �� 5^      -   5        $ CheckoutButton��5^      $��Checkout��5_      ��5`      �� $ CheckoutCommand�5c      5d      5e      