   M S B A M L   `   `   `  ����I  EPTC Management, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PTC_ManagementPTC_Management  5      ;"clr-namespace:PTC_Management.ViewsPTC_Management.Views  5      C&clr-namespace:PTC_Management.ViewModelPTC_Management.ViewModel  5          PTC_Management.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .localclr-namespace:PTC_Management     5      4views"clr-namespace:PTC_Management.Views     5      =
viewModels&clr-namespace:PTC_Management.ViewModel     5        � Title$  
MainWindow��5
      $	��400��6   $
��1000��6(    � DataContext 5      (  "PTC_Management.MainWindowViewModel  5   
    � 	Resources 5       �� DataType0  *PTC_Management.ViewModel.EmployeeViewModel'   !PTC_Management.Views.EmployeeView-  'PTC_Management.ViewModel.RouteViewModel$   PTC_Management.Views.RouteView1  +PTC_Management.ViewModel.TransportViewModel(   "PTC_Management.Views.TransportView4  .PTC_Management.ViewModel.ItineraryLogViewModel+	   %PTC_Management.Views.ItineraryLogView��%,  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5      	  5      5!      .��5#      �  � RowDefinitions 5$   
   �� 5%      $��50 �6   �� 5&      $��* �6   .+�5)   
   � -   5         �Name$ NavBar��5)       � ColumnDefinitions 5*      �� 5+      $��* �6#   �� 5,      $��* �6#   �� 5-      $��* �6#   �� 5.      $��* �6#   .+�52      �� $��Сотрудники��6   $��	employees��54      $��0��55      ��53      �� *
NavCommand+55      �� 57      $��Маршруты��6   $��routes��59      $��1��5:      ��58      �� *
NavCommand+5:      �� 5<      $��Транспорт��6   $��	transport��5>      $��2��5?      ��5=      �� *
NavCommand+5?      �� 5A      $.��(Журнал путевых листов��6   $��ItineraryLog��5C      $��3��5D      ��5B      �� *
NavCommand+5D      5F      � 5H   
   -   5        $ MainContent��5H      $��1��6&   .+�5I      �� ��6   �� *CurrentViewModel+5J      5L      5N      