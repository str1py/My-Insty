   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant/   )Instagram_Assistant.View.FeedLikePageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5        � 	Resources  5         LikeActionTemplate J� Opacity �� CornerRadius J� Effect-  'System.Windows.Media.Effects.BlurEffect   Radius   
KernelType �� Orientation  SecondColorBrush j� Stretch �� ImageSource  TextBlockThinStyle  Smaller!  FontAwesome.WPF.FontAwesome	  Name
   Icon � Triggers :� RoutedEvent �� TargetProperty g� Duration W� From	 W� To W� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness t� From	 t� To t� EasingFunction.  (System.Windows.Media.Animation.PowerEase   Power g� AccelerationRatio  likeCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName  g� AutoReverse! g� RepeatBehavior  	ColorAnim  SecondColor	" �� To��%e  &	        &	 `    &	 �    &	 �    �� 5   
   .R�5      � .+�5      �� 
�������$
 0.05Q�6,   $ 5��6;    5        5      $ 10Q�6%   $ Gaussian=�61   5      �� 5      
������ $ 5��62   ._�5      �� $ Vertical=�6!   $	��150��68   $	��230��6D   .+�5      �� $��3,5,0,5q�6%   $ 
Horizontal=�66   $
��Left=�6O   .+�5      A� $��25��6'   $��25��62   $��2��6>   #���  6R   ��5   "   �� 5   &   $ Uniform=�6V    61   �� *AccountImage+6V   5      �� 5      $��5,0,0,0q�6(   #���  5   (   #���  5   (   $��Center=�5    (   
���������5   (   �� *AccountName+5!   (   5"      �� 5#      $	��160��6    $	��150��6-   $��UniformToFill=�6V   ��69   �� *	PostImage+6p     5$      $	 Action��6)   
���  ��
 6L   �� *Action+ 5%      :� 5&   "   $ Loaded��6/   [�5'   &   �� 5(   *   .��5)   .   �� .��5*   2   W� $ FontSize�6B   $ 0:0:3D�5+   B   $ 40X�5,   B   $ 20X�6L    5-   6     5.   :   $ EaseIn=�6F   $ 2��6Z   $ 1Q�6k   50   3   t� 51   2   $ Margin�6E   $ 0:0:3.5D�52   E   $ 
0,-100,0,0X�53   E   $ 0,10,0,0X�6W    54   6     55   :   $ EaseIn=�6D   $ 15Q�6X   6f   57   3   58   /   59   +   5;   #   5=       5?      :� 5@      $ Loaded��6+   [�5A   "   �� 5B   &   .��5C   *   �� .��5D   .   t� $ Margin�6A   $ 
-100,0,0,0X�5E   9   $ 0X�6K   $	 0.9Q�5F   9   $ 0:0:0.5D�6Q    5G   2     5H   6   $ EaseOut=�6B   $ 2��6W   $ 7Q�6h   5J   /   5K   +   5L   '   5N      5P      5Q      5R      5S      �� 5U   
    66   �� $ Actions� 5V        5W      $
 Time��6&   $ 
Descending=�6:   5Y      �� 5[   
   .��5\      W� $ Status��6   $ Opacity�5]      $ 1X�5^      $	 0.3X�5_        . $ 0:0:1D�5a      $! Forever��5b      5c      5d      �� 5e   
   .��5f      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5g      $! Forever��5h        . $ 0:0:1D�5j      #" �  5k      5l      5m      5n      .��5p      � # � RowDefinitions# 5q   
   �� 5r      �� 5s      �� 5t      �� 5u      �� 5v      �� 5w      �� 5y      �� 5z      �� 5{      �� 5|      �� 5}      �� 5~      �� 5      �� 5�      �� 5�      $ � ColumnDefinitions$ 5�   
   �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�   
   �� $��0��6   $��32��6$   $��4��69   $��8��6J     TextBlockMainStyle#���  5�      $��Center=�5�      $��Center=�61    	 H1#��� 	 5�      
Overview6?   �� 5�   
   $��0��6   $��5��6$   $��2��68   $��0��6I   #���  5�       
 Regular#��� 
 5�      Last actions6G   �� 5�   
   $��0��6   $��11��6!   $��11��66   $��3��6D   $ 3��5�      
�������$
 0.05Q�5�       5�        5�      $ 10Q�6   $ Gaussian=�6)   5�      �� 5�   
   $��21��6   $��15��6"   $��11��67   $��3��6E   $ 3��6V   
�������$
 0.05Q�5�       5�        5�      $ 10Q�6   $ Gaussian=�6)   5�      �� 5�   
   $��11��6   $��14��6#   $��4��64   $��3��6H   $��Center=�6Z   $ Vertical=�6u   .+�5�      ��   MainButtonStyle#���  6   $��40��5�      ��5�      �� *StartCommand+��5�      �� *ButtonContent+�� 5�      -   5        % ��Name$% Status��5�      $��Center=�5�      
�������$��0,20,0,5q�5�       
 Bigger#���  5�      #���  5�      ��5�      �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction&   Triggers& 5�      /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5�      '   Value$' WORKING��6$   :	  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger( 	  Binding( 64   �� *Stats.Status+*
  $Microsoft.Xaml.Behaviors.TriggerBase) 
  Actions.) 5�      <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction*   
Storyboard#* [ 64   +   ControlStoryboardOption$
+ Play=�6]   5�        5�      $	' OFF��6$   ( 60   �� *Stats.Status+.) 5�        #* [ 64   $
+ Stop=�6]   5�      5�      5�      �� 5�   
   $��11��6   $��3��6#   $��1��64   $��10��6D   $��Center=�6Y   $ 
Horizontal=�6t   .+�5�      �� $��Center=�6   .+�5�      �� $ 
Horizontal=�6   .+�5�      �� $ Vertical=�6!   $��0,0,40,0q�68   .+�5�      �� $ 
Horizontal=�6%   $��0,0,0,5q�6>   .+�5�        $��0,0,5,0q�6-   $
 Heart=�5�   -   $��15(�5�   -   
���  ��$��Center=�5�   -   $��Center=�5�   -   �� 5�      $��Center=�6(   
�������#���  5�   (     TextBlockExtraThinStyle#���  5�   (   ��5�   (   �� *Stats.SessionCount+5�      �� 5�      $��Center=�6%   #���  5�   %   $��Session��5�   %   5�      �� 5�      $ Vertical=�6"   $��0,0,40,0q�69   .+�5�      �� $ 
Horizontal=�6%   $��0,0,0,5q�6>   .+�5�        $��0,0,5,0q�6-   $
 Heart=�5�   -   $��15(�5�   -   
���  ��$��Center=�5�   -   $��Center=�5�   -   �� 5�      $��Center=�6(   
�������#���  5   (   #���  5  (   ��5  (   �� *Stats.Count+5     �� 5     $��Center=�6$   #���  5  $   $��Total��5  $   5     �� 5	     $��Center=�6!   .+�5
     �� $��Center=�6$   $��0,0,0,5q�5  $   
�������#���  5  $   #���  5  $   ��5  $   �� *Stats.NextIn+�� 5     $��Center=�6$   #���  5  $   $��Next in��6Q   5     5     5     5     �� 5  
   $��11��6   $��3��6#   $��22��64   $��9��6E   $��Center=�6Y   $��Center=�6t   $ 
Horizontal=�6�   .+�5     �� $��Center=�6   $��0,0,40,0q�66   .+�5     �� $��Center=�6   $��0,0,0,5q�5     
�������#���  5     #���  5     ��5     �� *Stats.TimeInWork+�� 5     $��Center=�6   #���  5      $��Time in work��5!     5"     �� 5$     $��Center=�6   .+�5%     �� $��Center=�6   $��0,0,0,5q�5&     
�������#���  5(     #���  5)     ��5*     �� *Stats.NextSessionIn+, �� StringFormat$, hh\:mm���� 5+     $��Center=�6   #���  5,     $��Next session in��5-     5.     5/     �� 52  
   $��4��6   $��0��6!   $��32��61   $��2��6F   $	 0.6Q�53  :     TextBlockH2Style#���  54     #��� 	 55     $��Center=�56     $��Center=�6/   ��53     �� *LastActionTextHelper+56  /   �� 59  
   $��1��6   $��8��6    $��0��61   $��32��6A   
������ $��0q�6p   #���   5:  S   ��.  $��Hidden=�5;  9   $��Hidden=�5<     !- �� IsDeferredScrollingEnabled- .  ��5:     �� . �� Source#. [ ��5=     �� 5>     .R�5?     $� / $� Orientation$/ 
Horizontal=�6-   5@     5C     5E     5F     