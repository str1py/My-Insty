   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant.   (Instagram_Assistant.View.GeoLikePageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5        � 	Resources  5         LikeActionTemplate J� Opacity �� CornerRadius J� Effect-  'System.Windows.Media.Effects.BlurEffect   Radius   
KernelType �� Orientation  SecondColorBrush j� Stretch �� ImageSource  TextBlockThinStyle  Smaller!  FontAwesome.WPF.FontAwesome	  Name
   Icon � Triggers :� RoutedEvent �� TargetProperty g� Duration W� From	 W� To W� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness t� From	 t� To t� EasingFunction.  (System.Windows.Media.Animation.PowerEase   Power g� AccelerationRatio  likeCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	PanelShow �� 
TargetName  	PanelHide  	FlashAnim  g� AutoReverse! g� RepeatBehavior  	ColorAnim 	 SecondColor	" �� To��%�	  &	        &	 `    &	 �    &	 u    &	     &	 �    �� 5   
   .R�5      � .+�5      �� 
�������$
 0.05Q�6,   $ 5��6;    5        5      $ 10Q�6%   $ Gaussian=�61   5      �� 5      
������ $ 5��62   ._�5      �� $ Vertical=�6!   $	��150��68   $	��230��6D   .+�5      �� $��3,5,0,5q�6%   $ 
Horizontal=�66   $
��Left=�6O   .+�5      A� $��25��6'   $��25��62   $��2��6>   #���  6R   ��5   "   �� 5   &   $ Uniform=�6V    61   �� *AccountImage+6V   5      �� 5      $��5,0,0,0q�6(   #���  5   (   #���  5   (   $��Center=�5   (   
���������5   (   �� *AccountName+5    (   5!      �� 5"      $	��160��6    $	��150��6-   $��UniformToFill=�6V   ��69   �� *	PostImage+6p     5#      $	 Action��6)   
���  ��
 6L   �� *Action+ 5$      :� 5%   "   $ Loaded��6/   [�5&   &   �� 5'   *   .��5(   .   �� .��5)   2   W� $ FontSize�6B   $ 0:0:3D�5*   B   $ 40X�5+   B   $ 20X�6L    5,   6     5-   :   $ EaseIn=�6F   $ 2��6Z   $ 1Q�6k   5/   3   t� 50   2   $ Margin�6E   $ 0:0:3.5D�51   E   $ 
0,-100,0,0X�52   E   $ 0,10,0,0X�6W    53   6     54   :   $ EaseIn=�6D   $ 15Q�6X   6f   56   3   57   /   58   +   5:   #   5<       5>      :� 5?      $ Loaded��6+   [�5@   "   �� 5A   &   .��5B   *   �� .��5C   .   t� $ Margin�6A   $ 
-100,0,0,0X�5D   9   $ 0X�6K   $	 0.9Q�5E   9   $ 0:0:0.5D�6Q    5F   2     5G   6   $ EaseOut=�6B   $ 2��6W   $ 7Q�6h   5I   /   5J   +   5K   '   5M      5O      5P      5Q      5R      �� 5T   
    66   �� $ Actions� 5U        5V      $
 Time��6&   $ 
Descending=�6:   5X      �� 5Z   
   .��5[      t� $ Margin�6!   $ 0,420,0,-420X�5\   !   $ 0X�65   $ 	SidePanel��5]   !   $ .9Q�5^   !   $ 0:0:0.5D�68   5_      �� 5`   
   .��5a      t� $ Margin�6!   $ 0X�5b   !   $ 0,420,0,-420X�6*   $ 	SidePanel��5c   !   $ .9Q�5d   !   $ 0:0:0.5D�68   5e      �� 5g   
   .��5h      W� $ Status��6   $ Opacity�5i      $ 1X�5j      $	 0.3X�5k        . $ 0:0:1D�5m      $! Forever��5n      5o      5p      �� 5q   
   .��5r      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5s      $! Forever��5t        . $ 0:0:1D�5v      #" � 	 5w      5x      5y      5z      .��5|      � .+�5}   
   � # � RowDefinitions# 5~      �� 5      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      $ � ColumnDefinitions$ 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      .+�5�      �� $��0��6   $��32��6(   $��4��6=   $��8��6N    
 TextBlockMainStyle#��� 
 5�      $��Center=�5�      $��Center=�61     H1#���  5�      
Overview6?   �� 5�      $��0��6   $��5��6(   $��2��6<   $��0��6M   #��� 
 5�        Regular#���  5�      Last actions6G   �� 5�      $��0��6   $��11��6%   $��11��6:   $��3��6H   $ 3��5�      
�������$
 0.05Q�5�       5�        5�      $ 10Q�6!   $ Gaussian=�6-   5�      �� 5�      $��21��6   $��15��6&   $��11��6;   $��3��6I   $ 3��6Z   
�������$
 0.05Q�5�       5�        5�      $ 10Q�6!   $ Gaussian=�6-   5�      �� 5�      $��11��6   $��14��6'   $��4��68   $��3��6L   $��Center=�6^   $ Vertical=�6y   .+�5�      ��   MainButtonStyle#���  6   $��40��5�      ��5�      �� *StartCommand+��5�      �� *ButtonContent+�� 5�      -   5        % ��Name$% Status��5�      $��Center=�5�      
�������$��0,20,0,5q�5�       
 Bigger#���  5�      #���  5�      ��5�      �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction&   Triggers& 5�      /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5�      '   Value$' WORKING��6$   :	  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger( 	  Binding( 64   �� *Stats.Status+*
  $Microsoft.Xaml.Behaviors.TriggerBase) 
  Actions.) 5�      <  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction*   
Storyboard#* [ 64   +   ControlStoryboardOption$
+ Play=�6]   5�        5�      $	' OFF��6$   ( 60   �� *Stats.Status+.) 5�        #* [ 64   $
+ Stop=�6]   5�      5�      5�      �� 5�      $��11��6   $��3��6'   $��1��68   $��10��6H   $��Center=�6]   $ 
Horizontal=�6x   .+�5�      �� $��Center=�6   .+�5�      �� $ 
Horizontal=�6!   .+�5�      �� $ Vertical=�6%   $��0,0,40,0q�6<   .+�5�      �� $ 
Horizontal=�6)   $��0,0,0,5q�6B   .+�5�   "     $��0,0,5,0q�61   $
 Heart=�5�   -   $��15(�5�   -   
���  ��$��Center=�5�   -   $��Center=�5�   -   �� 5�   "   $��Center=�6,   
�������#���  5�   (     TextBlockExtraThinStyle#���  5�   (   ��5�   (   �� *Stats.SessionCount+5�      �� 5�      $��Center=�6)   #���  5   %   $��Session��5  %   5     �� 5     $ Vertical=�6&   $��0,0,40,0q�6=   .+�5     �� $ 
Horizontal=�6)   $��0,0,0,5q�6B   .+�5  "     $��0,0,5,0q�61   $
 Heart=�5  -   $��15(�5  -   
���  ��$��Center=�5
  -   $��Center=�5  -   �� 5  "   $��Center=�6,   
�������#���  5  (   #���  5  (   ��5  (   �� *Stats.Count+5     �� 5     $��Center=�6(   #���  5  $   $��Total��5  $   5     �� 5     $��Center=�6%   .+�5     �� $��Center=�6(   $��0,0,0,5q�5  $   
�������#���  5  $   #���  5  $   ��5  $   �� *Stats.NextIn+�� 5     $��Center=�6(   #���  5  $   $��Next in��6Q   5      5!     5"     5#     �� 5%     $��11��6   $��3��6'   $��22��68   $��9��6I   $��Center=�6]   $��Center=�6x   $ 
Horizontal=�6�   .+�5&     �� $��Center=�6   $��0,0,40,0q�6:   .+�5'     �� $��Center=�6    $��0,0,0,5q�5(     
�������#���  5*     #���  5+     ��5,     �� *Stats.TimeInWork+�� 5-     $��Center=�6    #���  5.     $��Time in work��5/     50     �� 52     $��Center=�6   .+�53     �� $��Center=�6    $��0,0,0,5q�54     
�������#���  56     #���  57     ��58     �� *Stats.NextSessionIn+, �� StringFormat$, hh\:mm���� 59     $��Center=�6    #���  5:     $��Next session in��5;     5<     5=     �� 5@     $��4��6   $��0��6%   $��32��65   $��2��6J   $	 0.6Q�5A  :   #��� 
 5B     #���  5C     $��Center=�5D     $��Center=�6/   ��5A     �� *LastActionTextHelper+5D  N   �� 5G     $��1��6   $��8��6$   $��0��65   $��32��6E   
������ $��0q�6t   #���   5H  S   ��.  $��Hidden=�5I  9   $��Hidden=�5J     !- �� IsDeferredScrollingEnabled- .  ��5H     �� . �� Source#. [ ��5K     �� 5L     .R�5M     $� / $� Orientation$/ 
Horizontal=�61   5N     5P     5Q     � 5R  
   -   5        0 �Name$0 	SidePanel��5R     $��0,420,0,-420q�6"   # 5S     �� 5T     �� 5U     �� 5V     �� 5W     �� 5X     �� 5Y     �� 5Z     �� 5[     �� 5\     �� 5]     �� 5^     �� 5_     �� 5`     �� 5a     �� 5b     $ 5d     �� 5e     �� 5f     �� 5g     �� 5h     �� 5i     �� 5j     �� 5k     �� 5l     �� 5m     �� 5n     �� 5o     �� 5p     �� 5q     �� 5r     �� 5s     �� 5t     �� 5u     �� 5v     �� 5w     �� 5x     �� 5y     �� 5z     �� 5{     �� 5|     �� 5}     �� 5~     �� 5     �� 5�     �� 5�     �� 5�     �� 5�     �� 5�     .+�5�     �� $��32��6   $��1��6*   $��15��67   
���   �$	 0.8Q�5�  (    5�       5�     $ 10Q�6!   $ Gaussian=�6-   5�     5�     �� 5�     $��4��6   
������ $��0q�5�       FontAwesome.WPF.Awesome1   Content$1 LocationArrow=�5�     $��14��5�     #���  5�     $��25(�6?   y�5�     �� *StPanelShowVisibility+��5�     �� *StPanelShowCommand+ 5�     :� 5�     $ Button.Click��6#   .[�5�     �� #���  6*   5�     5�     �� 5�     $��4��6   
������ $��0q�5�     $1 LocationArrow=�5�     $��14��5�     #���  5�     $��25(�6?   y�5�     �� *StPanelHideVisibility+��5�     �� *StPanelHideCommand+ 5�     :� 5�     $ Button.Click��6#   .[�5�     �� #���  6*   5�     5�     �� 5�     $��2��6   $��6��6&   $��13��66   $��20��6H   .+�5�     �� $��Search location��6   �� 5�     $��9��6   $��2��6(   $��10��69   $��8��6J   2 �� TextAlignment$2 Center=�5�       MyWaterMarkStyle#���  5�     $��30(�5�     
3 � Tag$3 Enter loaction...��6&   ��5�     �� *	GeoString+4 �� UpdateSourceTrigger$4 PropertyChanged=�6X   �� 5�     
������ 
������ $	��300��5�     $��Regular"�5�     
�������  LogListStyle#���  5�     ��5�     �� *SearchResults+5 �� SelectedItem5 5�     �� *SelectedGeoItem+��5�     �� 5�     .R�5�     �� $��5q�6*   $ 
Horizontal=�65   .+�5�  "   "  FontAwesome.WPF.ImageAwesome  6   Icon$6 LocationArrow=�62   $��0,0,5,0q�6G   $��15��6X   7   
Foreground
7 �����$
��Left=�6v   �� 5�  "   $��16(�6,   $	��350��6P   $
��Left=�6\   ��6:   �� *Name+6\   5�     5�     & 5�     +  %Microsoft.Xaml.Behaviors.EventTrigger  5�     8   	EventName$8 MouseDoubleClick��6)   .) 5�     2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  9   Command9 64   �� *SelectGeoCommand+5�     5�     5�       5�     $��8��6   $��15��6+   $��2��6<   $��2��6M   $6 Spinner=�5�     #7 [ 5�     :   Spin: . $��50��5�     $��50��6)   y�5�     �� *LoadingVisibility+5�  )   5�     5�     5�     