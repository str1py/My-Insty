   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant*   $Instagram_Assistant.View.SpyPageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5         UnfollowAction  BorderColor �� CornerRadius �� Orientation!  FontAwesome.WPF.FontAwesome   Icon j� Stretch �� ImageSource � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  unfollowCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%c  &	        7  &	 Z    &	 �    &	     �� 5   
   .R�5      �� 8��  $ 5��6?   ._�5      �� $	��480��6   $��50��6)   $ 
Horizontal=�66   .+�5        $��Center=�6%   $��Center=�5   %   $��20(�5   %   $��2,0,0,0q�5   %   $��25��5   %   
������� 5   8   �� *Action+A� 5      $��10,0,0,0q�6   $��25��60   $��25��6;   $��0��6G   
���������5      �� 5      $ Uniform=�6O    6)   �� *AccountImage+6O   5      �� 5      $��5,0,10,0q�6    
�������$��12(�5       $
��auto��5        $��Center=�5!       $��
ExtraLight"�5"       ��5   2   �� *AccountName+5"   :   �� 5$      
�������$��12(�5&       $��WrapWithOverflow=�5'       $	��380��5(       $��Center=�5)       ��5$       �� *Status+5*   #    5-      :� 5.      $ Loaded��6'   [�5/      �� 50   "   .��51   &   �� .��52   *   t� $ Margin�6=   $	 
-100,0,0,0X�53   9   $
 0X�6K   $	 0.9Q�54   9   $ 0:0:0.5D�6Q    55   .     56   2   $ EaseOut=�6>   $ 2��6S   $ 7Q�6d   58   +   59   '   5:   #   5<      5?      5@      5A      �� 5C   
    6:   �� $ Actions� 5D        5E      $
 Time��6&   $ 
Descending=�6:   5G      �� 5I   
   .��5J      W� $ Status��6   $ Opacity�5K      $ 1X�5L      $	 0.3X�5M       . $ 0:0:1D�5O      $ Forever��5P      5Q      5R      �� 5S   
   .��5T      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5U      $ Forever��5V       . $ 0:0:1D�5X      # �  5Y      5Z      5[      5\      .��5^      �  � RowDefinitions 5_   
   �� 5`      �� 5a      �� 5b      �� 5c      �� 5d      �� 5e      �� 5f      �� 5g      �� 5h      �� 5i      �� 5j      �� 5k      �� 5l      �� 5m      �� 5n       � ColumnDefinitions 5p   
   �� 5q      �� 5r      �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5x      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      �� 5      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�   
   �� $��21��6   $��21��6%   $��1��6;   $��0��6L     TextBlockMainStyle#���  5�      $��Center=�5�        H1#���  5�      $��Overview��64   �� 5�   
   $��0��6   $��5��6$   $��2��68   $��0��6I   #���  5�        Regular#���  5�      $��Last actions��5�      �� 5�   
   $��21��6   $��11��6"   $��11��67   $��3��6E   
�������$ 3��5�       J� Opacity$
 0.05Q�5�       J� Effect 5�      -  'System.Windows.Media.Effects.BlurEffect  5�          Radius$  10Q�6   !   
KernelType$! Gaussian=�6)   5�      �� 5�   
   $��1��6   $��24��6"   $��5��63   $��10��6G   $��Center=�6Z   $ Vertical=�6u   .+�5�      ��  	 MainButtonStyle#��� 	 6   $��40��5�      ��5�      �� *StartCommand+��5�      �� *ButtonContent+�� 5�      -   5        " ��Name$" Status��5�      $��Center=�6(   
�������$��0,20,0,5q�5�       

 Bigger#��� 
 5�        TextBlockThinStyle#���  5�      ��5�      �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction#   Triggers# 5�      /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5�      $   Value$$ WORKING��6$   :  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger%   Binding% 64   �� *Stats.Status+*	  $Microsoft.Xaml.Behaviors.TriggerBase& 	  Actions.& 5�      <
  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction
  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction'   
Storyboard#' [ 64   ( 
  ControlStoryboardOption$
( Play=�6]   5�        5�      $	$ OFF��6$   % 60   �� *Stats.Status+.& 5�      
  #' [ 64   $
( Stop=�6]   5�      5�      �� 5�      #���  6   $��Center=�5�      $��
ExtraLight"�5�      Spy account6=   �� 5�      $��Center=�6   
�������$��15(�5�      $��0,0,0,5q�5�      $��
ExtraLight"�5�      ��60   �� *Stats.TechAccount+�� 5�      #���  6   $��Center=�5�      $��
ExtraLight"�5�      Main account6>   �� 5�      $��Center=�6   
�������$��13(�5�      $��0,0,0,15q�5�      $��
ExtraLight"�5�      ��60   �� *Stats.MainAccount+�� 5�      $��Spy on��6   #���  6&   �� 5�        ComboBoxStyle#���  6   ) �� SelectedIndex) 6?   �� *ComboBoxSelectedIndex+.@�5�      �� $��All followings��6   �� 5�      $��All followers��6   �� 5�      $��Besty��6   �� 5�      $��From user list��6   5�      �� 5�      $��11��6   $��3��6(   $��21��69   $��13��6J   $��Center=�5�      $��Center=�5�      $ 
Horizontal=�5�      $��0,10,0,0q�5�      .+�5�      �� $��Center=�6   $��0,0,30,0q�5�      .+�5�      �� $��Center=�6    
�������$��0,0,0,5q�5�      #��� 
 5�      #���  5�      ��5�      �� *Stats.StoriesCount+�� 5�      $��Center=�6    #���  5�      	Stories6R   5�      �� 5�      $��Center=�6   .+�5�      �� $��Center=�6    
�������$��0,0,0,5q�5�      #��� 
 5�      #���  5�      ��5�      �� *Stats.PostsCount+�� 5�      $��Center=�6    #���  5�      Posts6P   5�      5�      5�      �� 5�   
   $��11��6   $��3��6#   $��21��64   $��13��6E   $��Center=�5�      $��Center=�5�      $ 
Horizontal=�5�      .+�5�      �� $��Center=�6   $��0,0,30,0q�5�      .+�5�      �� $��Center=�6   
�������$��0,0,0,5q�5      #��� 
 5     #���  5     ��5     �� *Stats.TimeInWork+�� 5     $��Center=�6   #���  5     Time in work6W   5     �� 5     $��Center=�6   .+�5	     �� $��Center=�6   
�������$��0,0,0,5q�5     #��� 
 5     #���  5     ��5     �� *Stats.NextSessionIn+�� 5     $��Center=�6   #���  5     Timer6P   5     5     �� 5  
   $��5��6   $��0��6!   $��20��61   $��2��6F   $	 0.6Q�5  :   #���  5     #���  5     $��Center=�5     $��Center=�5     ��5     �� *LastActionTextHelper+5     �� 5  
   $��1��6   $��13��6    $��0��62   $��20��6B   
������ $��0q�5!     #���   5#     ��.  $��Hidden=�5%     $��Hidden=�5&     !* �� IsDeferredScrollingEnabled* .  ��5"     �� + �� Source#+ [ 5(     5)     5*     