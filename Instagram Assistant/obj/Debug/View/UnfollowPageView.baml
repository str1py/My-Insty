   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant/   )Instagram_Assistant.View.UnfollowPageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5         UnfollowAction  BorderColor �� CornerRadius �� Orientation j� Stretch �� ImageSource!  FontAwesome.WPF.FontAwesome   Icon � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  unfollowCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%K  &	        7  &	 B    &	 �    &	 g    �� 5   
   .R�5      �� 8��  $ 5��6?   ._�5      �� $	��480��6   $��50��6)   $ 
Horizontal=�66   .+�5      A� $��10,0,0,0q�6   $��25��60   $��25��6;   $��2��6G   
���������5      �� 5      $ Uniform=�6O    6)   �� *AccountImage+6O   5      �� 5      $��5,0,0,0q�6    
�������$��12(�5       $	��200��5       $��Center=�5       $��
ExtraLight"�5       ��5   1   �� *AccountName+5   :   �� 5      
�������$��12(�5       $	��100��5   !   $��Center=�5        $��
ExtraLight"�5!       ��5   !   �� *Status+5!   :     5#      $��2��6%   $��Center=�5$   %   $��20(�5%   %   $��	100,0,0,0q�5&   %   
������� 5'   8   �� *Action+6R    5)      :� 5*      $ Loaded��6'   [�5+      �� 5,   "   .��5-   &   �� .��5.   *   t� $ Margin�6=   $	 
-100,0,0,0X�5/   9   $
 0X�6K   $	 0.9Q�50   9   $ 0:0:0.5D�6Q    51   .     52   2   $ EaseOut=�6>   $ 2��6S   $ 7Q�6d   54   +   55   '   56   #   58      5;      5<      5=      �� 5?   
    6:   �� $ Actions� 5@        5A      $
 Time��6&   $ 
Descending=�6:   5C      �� 5E   
   .��5F      W� $ Status��6   $ Opacity�5G      $ 1X�5H      $	 0.3X�5I       . $ 0:0:1D�5K      $ Forever��5L      5M      5N      �� 5O   
   .��5P      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5Q      $ Forever��5R       . $ 0:0:1D�5T      # �  5U      5V      5W      5X      .��5Z      �  � RowDefinitions 5[   
   �� 5\      �� 5]      �� 5^      �� 5_      �� 5`      �� 5a      �� 5b      �� 5c      �� 5d      �� 5e      �� 5f      �� 5g      �� 5h      �� 5i      �� 5j       � ColumnDefinitions 5l   
   �� 5m      �� 5n      �� 5o      �� 5p      �� 5q      �� 5r      �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      �� 5      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�   
   �� $��21��6   $��21��6%   $��2��6;   $��0��6L     TextBlockMainStyle#���  5�      $��Center=�5�        H1#���  5�      $��Overview��64   �� 5�   
   $��0��6   $��5��6$   $��2��68   $��0��6I   #���  5�        Regular#���  5�      $��Last actions��5�      �� 5�   
   $��21��6   $��13��6"   $��6��67   $��3��6D   
�������$ 3��5�       J� Opacity$
 0.05Q�5�       J� Effect 5�      -  'System.Windows.Media.Effects.BlurEffect  5�          Radius$  10Q�6   !   
KernelType$! Gaussian=�6)   5�      �� 5�   
   $��21��6   $��11��6"   $��11��67   $��3��6E   
�������$ 3��5�      $
 0.05Q�5�       5�        5�      $  10Q�6   $! Gaussian=�6)   5�      �� 5�   
   $��2��6   $��24��6"   $��5��63   $��3��6G   $��Center=�6Y   $ Vertical=�6t   .+�5�      ��  	 MainButtonStyle#��� 	 6   $��40��5�      ��5�      �� *StartCommand+��5�      �� *ButtonContent+�� 5�      -   5        " ��Name$" Status��5�      $��Center=�6(   
�������$��0,20,0,5q�5�       

 Bigger#��� 
 5�        TextBlockThinStyle#���  5�      ��5�      �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction#   Triggers# 5�      /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5�      $   Value$$ WORKING��6$   :  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger%   Binding% 64   �� *Stats.Status+*	  $Microsoft.Xaml.Behaviors.TriggerBase& 	  Actions.& 5�      <
  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction
  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction'   
Storyboard#' [ 64   ( 
  ControlStoryboardOption$
( Play=�6]   5�        5�      $	$ OFF��6$   % 60   �� *Stats.Status+.& 5�      
  #' [ 64   $
( Stop=�6]   5�      5�      5�      �� 5�   
   $��6��6   $��3��6"   $��22��63   $��9��6D   $��Center=�5�      $ 
Horizontal=�5�      .+�5�      �� $��Center=�6   .+�5�      �� $ 
Horizontal=�6   .+�5�      �� $ Vertical=�6!   $��0,0,40,0q�5�   !   .+�5�      �� $ 
Horizontal=�6%   .+�5�        $��0,0,5,0q�6-   $ 	UserTimes=�5�   -   $��15(�5�   -   
�������$��Center=�5�   -   $��Center=�5�   -   �� 5�      $��Center=�6(   
�������#��� 
 5�   (   #���  5�   (   ��5�   (   �� *Stats.SessionCount+5�      �� 5�      $��Center=�6%   #���  5�   %   	Session6[   5�      �� 5�      $ Vertical=�6!   $��0,0,40,0q�5�   !   .+�5�      �� $ 
Horizontal=�6%   .+�5�        $��0,0,5,0q�6-   $ 	UserTimes=�5�   -   $��15(�5�   -   
�������$��Center=�5�   -   $��Center=�5�   -   �� 5�      $��Center=�6(   
�������#��� 
 5�   (   #���  5�   (   ��5�   (   �� *Stats.Count+5�      �� 5�      $��Center=�6$   #���  5�   $   Total6X   5�      �� 5�      $��Center=�6!   .+�5�      �� $��Center=�6$   
�������#��� 
 5�   $   #���  5�   $   ��5�   $   �� *Stats.NextIn+�� 5�      $��Center=�6$   #���  5�   $   	Next in6Z   5�      5�      5�      5�      �� 5�   
   $��11��6   $��3��6#   $��21��64   $��13��6E   $��Center=�5      $��Center=�5     $ 
Horizontal=�5     .+�5     �� $��Center=�6   $��0,0,30,0q�5     .+�5     �� $��Center=�6   
�������$��0,0,0,5q�5     #��� 
 5     #���  5	     ��5
     �� *Stats.TimeInWork+�� 5     $��Center=�6   #���  5     Time in work6W   5     �� 5     $��Center=�6   .+�5     �� $��Center=�6   
�������$��0,0,0,5q�5     #��� 
 5     #���  5     ��5     �� *Stats.NextSessionIn+�� 5     $��Center=�6   #���  5     Rest6O   5     5     �� 5  
   $��5��6   $��0��6!   $��20��61   $��2��6F   $	 0.6Q�5  :   #���  5     #���  5     $��Center=�5      $��Center=�5!     ��5     �� *LastActionTextHelper+5!     �� 5$  
   $��1��6   $��13��6    $��0��62   $��20��6B   
������ $��0q�5&     #���   5(     ��.  $��Hidden=�5*     $��Hidden=�5+     !) �� IsDeferredScrollingEnabled) .  ��5'     �� * �� Source#* [ 5-     5.     5/     