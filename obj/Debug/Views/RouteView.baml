   M S B A M L   `   `   `  ����I  EPTC Management, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PTC_Management.ViewsPTC_Management.Views  5      $    PTC_Management.Views.RouteView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PTC_Management.Views     5        � 	Resources  5	         CenterGridHeaderStyle �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� Property �� Value  BlackButton �� 
TargetType �Name �� RadiusX �� RadiusY J� BitmapEffect	 �� Radius
 J� RenderTransformOrigin T� X T� Y �� ScaleX �� ScaleY ��Name �� 
StartPoint �� EndPoint � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 L     �� 5
   
   "  6.   .�5      ��  � ��$ Center=�6;   5      �� 5   
   " ��6   .�5      ��  � ��$ 80��6&   �� 5       � ��$ 24��6'   �� 5       � ��$ 0,0,10,0q�6'   �� 5       � ��
 �   �6+   �� 5       � ��
 �����6+   �� 5       � �� 5      �� 5      " ��6&   .R�5      � $ RootElement��6   .+�5      �� ���	#40000000$��1,1,0,0q�69   $ 6��6J   $ 6��6V    5   "   �� 5   &   $	 3Q�67   5      � 5      $ 
mainButton��6#   $��0,0,1,1q�5   !   $
 0.5,0.5&�5   !   z�5    "   V� 5!   &   .~�5"   *   T� $	 0.0Q�6=   $	 0.0Q�6E   �� 5#   *   $	 1.0Q�69   $	 1.0Q�6F   5$   '   .+�5&   "   �� $ mainRect��6,   #��z��6>   $ 6��6b   $ 6��6n   5'   #   �� 5)   "   $ glowRect��6,   $��1,1,1,1q�6>   $ 5��6O   $ 5��6[   ��5*   &   �� 5+   *   $	 0,0&�6>   $	 0,1&�6O   .��5,   .   � $ 0Q�6;   $ 	#EFFFFFFF��6F   � 5-   .   $ 1Q�6;   $ 	#00FFFFFF��6F   5.   +   50   #   �� 52   "   $��Center=�63   $��4,0,4,0q�53      #��z��54      $��Center=�55      57      58       59      P� 5:       �@J�IsMouseOver . . �5;   "   �� $ 
mainButton��6)    � z� 5<   &   V� 5=   *   .~�5>   .   T� $	 0.0Q�6A   $	 0.0Q�6I   �� 5?   .   $
 1.01Q�6=   $
 1.01Q�6K   5@   +   5B   #   �� 5C   "    � �� �	#FF4788c86?   5D      P� 5F       � �� . . �5G   "   �� $ 
mainButton��6)    � z� 5H   &   V� 5I   *   .~�5J   .   T� $ 0Q�6A   $	 0.5Q�6G   �� 5K   .   $
 1.01Q�6=   $
 1.01Q�6K   5L   +   5N   #   5O      5Q      5S      5T      5V      .��5W      �  � RowDefinitions 5X   
   �� 5Y      $��8* �6   �� 5Z      $
��217* �6    � ColumnDefinitions 5\   
   �� 5]      $��7* �6   �� 5^      $
��192* �6   .+�5`   
   �� $��2��6   $��2��6)   .+�5a      �� $��0,5,0,5q�6    �� Orientation$ 
Horizontal=�6*   .+�5b      �� -   5        $��create��5b      #��[ 6'   $��Создать��6L   �� 5c      -   5        $��change��5c      #��[ 6'   $��Изменить��6L   �� 5d      -   5        $��delete��5d      #��[ 6'   $��Удалить��6L   �� 5e      -   5        $
��copy��5e      #��[ 6'   $��Копировать��6L   �� 5g      $��Поиск:��6   $��5q�6*   �� 5h      $	��200��6`   $��5q�6l   ��6   �� *FilterRouteText+ �� UpdateSourceTrigger$ PropertyChanged=�6l   5i      &   System.Windows.Controls.DataGrid  5k      $	��400��6      ColumnHeaderStyle# [  6'      CanUserReorderColumns .     
IsReadOnly .    CanUserAddRows .      AutoGenerateColumns  .  ��5l   .   �� *
RouteItems+!   Columns! 5m      0  *System.Windows.Controls.DataGridTextColumn  5n      ,  &System.Windows.Controls.DataGridColumn"   Header$" Код��6)   1  +System.Windows.Controls.DataGridBoundColumn#   Binding# 6<   �� *	idRoute+  5o      $" Название��6)   # 6<   �� *name+  5p      $" Дистанция��6)   # 6<   �� *	distant+5r      5t      5u      5v      