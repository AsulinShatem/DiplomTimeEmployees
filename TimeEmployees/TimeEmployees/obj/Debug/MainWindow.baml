   M S B A M L   `   `   `  ����H  DTimeEmployees, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TimeEmployeesTimeEmployees  5   	       TimeEmployees.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TimeEmployees     5   	   -   5          � Title$  Руководитель��5   	   $	��450��6   $	��800��6+    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�6   .��5
      � .+�5   
   o� $	��Top=�6   $
��Left=�6'   .@�5      m� $��Справочники��6   .@�5      m� -   5        $��Отделы��5      6+   m� 5      -   5        $��Сотрудники��5      6/   5      m� 5      -   5        $��
Выход��5      6&   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dg��5      $��Center=�6   $	��329��6<   $	��Top=�6I   $	��780��6a      
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6   $��0,95,0,0q�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtStart��5      $
��Left=�6&   $��	54,61,0,0q�6A   $	��Top=�6T   $	��208��6l   �� 5   
   $��с��6   $
��Left=�6   $��	22,59,0,0q�67   $	��Top=�6J   �� 5   
   $��Фильтр��6   $
��Left=�6!   $��	22,33,0,0q�6<   $	��Top=�6O     5   
   -   5        $ dtEnd��5      $
��Left=�6$   $��
334,61,0,0q�6?   $	��Top=�6S   $	��208��6k   �� 5   
   $��по                ��6   $
��Left=�5      $��
302,59,0,0q�6-   $	��Top=�6A   �� 5   
   -   5        $��Применить��5      $
��Left=�6%   $��
559,61,0,0q�6@   $	��Top=�6T   $��24��6l   $	��126��6x   6�   �� 5   
   -	   5        $��X��5      $
��Left=�6   $��
706,60,0,0q�68   $	��Top=�6L   $��24��6d   $��42��6p   6{   5      5      