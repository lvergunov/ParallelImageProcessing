   M S B A M L   `   `   `  ����E  AUserInterf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UserInterf
UserInterf  5   	   %    UserInterf.ProcessedImageWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:UserInterf     5   	   -   5          � Title$  ProcessedImageWindow��5   	   $	��864��6&   $
��1536��63   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6   .+�5      �� $��0��6   
������� �� Padding$ 5q�69   ._�5      �� $H��BОднопоточная обработка изображения��6   $��30(�5      $
��Bold"�5      $��Center=�5      $��Center=�65   5      5      � 5   
   $��1��6   $��0 20 0 5q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5!      � $��1��6   $	��900��6#   $	��550��6/   .+�5"      �� -   5         ��Name$ ProcessedImageField��5"      5#      5$      � 5%   
   $��2��6   $��0 10 0 0q�6   .+�5&      �� -   5         ��Name$ OutputTextBlock��5&      $	��700��61   $��50��6=   $��20(�6I   $
��Wrap=�6W    �� TextAlignment$ Center=�6k   5'      � 5(   
   $��3��6   $��20q�6    5)      �� 5*      $��* �6#   �� 5+      $��* �6#   .+�5.      � $��0��6   .+�5/      �� -   5        $	��280��5/      $��60��6%   .��50      �� $��Закрыть��6    $��20(�6/   51      52      � 54      $��1��6   .+�55      �� -   5         ��Name$ 	SaveImage��55      $	��280��6,   $��60��68   .��56      �� $/��)Сохранить изображение��6    $��20(�6=   57      58      59      5:      5;      