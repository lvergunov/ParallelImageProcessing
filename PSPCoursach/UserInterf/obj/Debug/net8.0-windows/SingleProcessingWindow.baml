   M S B A M L   `   `   `  ����E  AUserInterf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UserInterf
UserInterf  5   	   '    !UserInterf.SingleProcessingWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:UserInterf     5   	     � Title$  SingleProcessingWindow��5   	   $
��1080��6(   $
��1920��66    � WindowState$ 	Maximized=�5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   
������� �� Padding$ 10q�65   $	��100��6B   ._�5      �� $H��BОднопоточная обработка изображения��6   $��36(�5      $
��Bold"�5      $��Center=�5      $��Center=�65   5      � 5   
   $��2��6    5      �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5       $��* �6#   .+�5"      � $��1��6   $	��700��6#   $
��1300��60   .+�5#      �� -   5         ��Name$ LoadedImage��5#      $y�	Collapsed=�6-   �� 5$      -   5         ��Name$ ButtonToLoadImg��5$      $	��550��62   $	��100��6>   .��5%      �� $/��)ЗАГРУЗИТЬ ИЗОБРАЖЕНИЕ��6    $��40(�6=   $
��Wrap=�6K   6a   5&      5'      5(      � 5)   
   $��3��6   $��60q�6    5*      �� 5+      $
��Auto �6     5-      �� 5.      $��* �6#   �� 5/      $
��Auto �6#   �� 50      $��* �6#   .+�52      � $��0��6   .+�53      �� -   5        $ LoadImageButton��53      $	��100��62   $	��600��6?   .��54      �� $%��ПРИМЕНИТЬ ФИЛЬТР��6    $��45(�68   $
��Wrap=�6F   55      56      � 57      $��2��6   .+�58      �� -   5        $ 
BackButton��58      $	��100��6-   $	��600��6:   .��59      �� $3��-ВЕРНУТЬСЯ В ГЛАВНОЕ МЕНЮ��6    $��40(�6@   $
��Wrap=�6N   5:      5;      5<      5=      5>      