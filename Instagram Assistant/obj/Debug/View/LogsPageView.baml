   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant+   %Instagram_Assistant.View.LogsPageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5        � 	Resources  5         UserMessage  BorderColor �� CornerRadius �� Orientation!  FontAwesome.WPF.FontAwesome  SecondColorBrush   Icon �� Padding  FontAwesome.WPF.Awesome   Content  Regular  MainFont � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  	logGroups �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction��%�  &	        7  7  7  7  &	 '    �� 5   
   .R�5      �� 8��  $��60��6?   $	��775��6N   $ 3��6Z   ._�5      �� $ 
Horizontal=�6   $��0,5,0,5q�66   .+�5      �� $ 
Horizontal=�6!   $��	15,0,15,0q�6:   .+�5        $��20(�5   )   $��Center=�5   )   8��  5   )   �� *icon+5   )   5      �� 5      $ Vertical=�6!   $��Center=�68   $	��100��6S   .+�5      �� $��Center=�5   $   $��15(�5   $   
�������$ 0q�5   $   ��5   $   �� *type+5   2   �� 5      
�������$ 	UserTimes=�5   7   $��Center=�5    $   $��10(�5!   $   ��5   $   �� *detailedtype+5!   $   5"      �� 5%      $ 
Horizontal=�6!   $��	15,0,15,0q�6:   .+�5&      �� 
�������$��15(�5(   $   $��Center=�5)   $   ��5&   $   �� *date+5)   $   5*      �� 5,      $ 
Horizontal=�6!   .+�5-      �� $��Center=�6$   $
��Wrap=�5.   $   $	��540��5/   $   
�������8�� 8�� ��51   $   �� *	message+53   $   54       55      :� 56      $ Loaded��6'   [�57      �� 58   "   .��59   &   �� .��5:   *   t� $ Margin�6=   $	 
-100,0,0,0X�5;   9   $
 0X�6K   $	 0.9Q�5<   9   $ 0:0:0.5D�6Q    5=   .     5>   2   $ EaseOut=�6>   $ 2��6S   $ 7Q�6d   5@   +   5A   '   5B   #   5D      5F      5H      5I      �� 5K   
    61   �� $ 
ActionList� 5L        5M      $
 date��6&   $ 
Descending=�6:   5O      5P      .��5S      �  � RowDefinitions 5T   
   �� 5U      �� 5V      �� 5W      �� 5X      �� 5Y      �� 5Z      �� 5[      �� 5\      �� 5]      �� 5^      �� 5_      �� 5`      �� 5a      �� 5b      �� 5c      �� 5d      �� 5e      �� 5f       � ColumnDefinitions 5h   
   �� 5i      �� 5j      �� 5k      �� 5l      �� 5m      �� 5n      �� 5o      �� 5p      �� 5q      �� 5r      �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      �� 5      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�   
   �� $��0��6   $��1��6!   $��1��62   $��6��6B   $
��LOGS��5�      $��Center=�5�      $��30(�5�      
�������  TextBlockMainStyle#���  5�      �� 5�   
   $��1��6   $��26��6$   $��1��69   $��16��6F   
������ 
������ #���   5�      ��5�      ��  �� Source# [ 5�      5�      5�      