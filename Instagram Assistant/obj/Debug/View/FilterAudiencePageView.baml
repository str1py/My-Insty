   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5       Instagram Assistant5   /Instagram_Assistant.View.FilterAudiencePageView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5        � 	Resources  5         AudienceAction  BorderColor �� CornerRadius �� Orientation j� Stretch �� ImageSource!  FontAwesome.WPF.FontAwesome   Icon � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  audienceCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%O  &	        7  &	 >    &	 �    &	 k    �� 5   
   .R�5      �� 8��  $	��550��6?   $ 5��6K   ._�5      �� $ 
Horizontal=�6   .+�5      �� $ Vertical=�6!   $	��150��68   $��Center=�6D   .+�5      A� $��40��6#   $��40��6.   $��2��6:   
���������5      �� 5   "   $ Uniform=�5   )    5   -   �� *AccountImage+5   )   5      �� 5      $��5,0,0,0q�6$   
�������$��10(�5       $��Center=�5       $��Center=�5       $��
ExtraLight"�5       ��5   5   �� *AccountName+5         5      $��2��6)   $��16(�5   %   $��0,5,0,0q�63   ��5   %   �� *ActionColor+ 6H   �� *Action+6b   5       �� 5!      $ Vertical=�6!   $��0,5,0,5q�68   $	��220��6I   .+�5"      �� $ 
Horizontal=�6%   .+�5#      �� $��5,0,0,0q�6(   $��Type: ��69   
�������$��12(�5%       $��55��5&       $��Center=�5'       $��
ExtraLight"�5(       �� 5)      $��5,0,0,0q�6(   $��12(�5+       $��Center=�5-       $��
ExtraLight"�5.       ��5)   9   �� *AccountType+��5*       �� *AccountTypeColor+5.       5/      �� 50      $ 
Horizontal=�6%   .+�51      �� $��5,0,0,0q�6(   $��
Categoty: ��69   
�������$��12(�53       $��55��54       $��Center=�55       $��
ExtraLight"�56       �� 57      $��5,0,0,0q�6(   $��12(�59       $��Center=�5:       $��
ExtraLight"�5;       ��57   9   �� *AccountCategory+��58       �� *AccountCategoryColor+5;       5<      �� 5=      $ 
Horizontal=�6%   .+�5>      �� $��5,0,0,0q�6(   $��City:  ��69   
�������$��12(�5@       $��55��5A   !   $��Center=�5B       $��
ExtraLight"�5C       �� 5D      $��5,0,0,0q�6(   $��12(�5F       $��Center=�5G       $��
ExtraLight"�5H       ��5D   9   �� *City+��5E       �� *	CityColor+5H       5I      �� 5J      $ 
Horizontal=�6%   .+�5K      �� $��5,0,0,0q�6(   $��Image:  ��69   
�������$��12(�5M       $��55��5N   !   $��Center=�5O       $��
ExtraLight"�5P       �� 5Q      $��5,0,0,0q�6(   $��12(�5S       $��Center=�5T       $��
ExtraLight"�5U       ��5Q   9   �� *
HasImage+��5R       �� *HasImageColor+5U       5V      �� 5W      $ 
Horizontal=�6%   .+�5X      �� $��5,0,0,0q�6(   $��Bio:  ��69   
�������$��12(�5Z       $��55��5[   )   $��Center=�5\       $��
ExtraLight"�5]       �� 5^      $��5,0,0,0q�6(   $��12(�5`       $��Center=�5a       $��
ExtraLight"�5b       ��5^   9   �� *HasBio+��5_       �� *HasBioColor+5b       5c      5d      �� 5e      $ Vertical=�6!   $��0,5,0,5q�68   $	��150��6I   .+�5f      �� $ 
Horizontal=�6%   .+�5g      �� $��5,0,0,0q�6(   $��Followers Count:  ��69   
�������$��12(�5i       $��90��5j   )   $��Center=�5k       $��
ExtraLight"�5l       �� 5m      $��5,0,0,0q�6(   $��12(�5o       $��Center=�5p       $��
ExtraLight"�5q       ��5m   9   �� *FollowersCount+��5n       �� *FollowersCountColor+5q       5r      �� 5s      $ 
Horizontal=�6%   .+�5t      �� $��5,0,0,0q�6(   $��Media Count:  ��69   
�������$��12(�5v       $��90��5w   )   $��Center=�5x       $��
ExtraLight"�5y       �� 5z      $��5,0,0,0q�6(   $��12(�5|       $��Center=�5}       $��
ExtraLight"�5~       ��5z   9   �� *
MediaCount+��5{       �� *MediaCountColor+5~       5      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��5,0,0,0q�6(   $��Highlight:  ��69   
�������$��12(�5�       $��90��5�   )   $��Center=�5�       $��
ExtraLight"�5�       �� 5�      $��5,0,0,0q�6(   $��12(�5�       $��Center=�5�       $��
ExtraLight"�5�       ��5�   9   �� *HasHighlight+��5�       �� *HasHighlightColor+5�       5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��5,0,0,0q�6(   $��Stop word:  ��69   
�������$��12(�5�       $��90��5�   )   $��Center=�5�       $��
ExtraLight"�5�       �� 5�      $��5,0,0,0q�6(   $��12(�5�       $��Center=�5�       $��
ExtraLight"�5�       ��5�   9   �� *HasStopWord+��5�       �� *HasStopWordColor+5�       5�      �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��5,0,0,0q�6(   $��
Go word:  ��69   
�������$��12(�5�       $��90��5�   )   $��Center=�5�       $��
ExtraLight"�5�       �� 5�      $��5,0,0,0q�6(   $��12(�5�       $��Center=�5�       $��
ExtraLight"�5�       ��5�   9   �� *	HasGoWord+��5�       �� *HasGoWordColor+5�       5�      5�       5�      :� 5�      $ Loaded��6'   [�5�      �� 5�   "   .��5�   &   �� .��5�   *   t� $ Margin�6=   $	 
-100,0,0,0X�5�   9   $
 0X�6K   $	 0.9Q�5�   9   $ 0:0:0.5D�6Q    5�   .     5�   2   $ EaseOut=�6>   $ 2��6S   $ 7Q�6d   5�   +   5�   '   5�   #   5�      5�      5�      5�      �� 5�   
    6:   �� $ AudienceActions� 5�        5�      $
 Time��6&   $ 
Descending=�6:   5�      �� 5�   
   .��5�      W� $ Status��6   $ Opacity�5�      $ 1X�5�      $	 0.3X�5�       . $ 0:0:1D�5�      $ Forever��5�      5�      5�      �� 5�   
   .��5�      �� $ Status��6   $( "Foreground.(SolidColorBrush.Color)�5�      $ Forever��5�       . $ 0:0:1D�5�      # �  5�      5�      5�      5�      .��5�      �  � RowDefinitions 5�   
   �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�       � ColumnDefinitions 5�   
   �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5�      �� 5      �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5     �� 5	     .+�5  
   �� $��22��6   $��4��6%   $��2��69   $��0��6J     TextBlockMainStyle#���  5     $��Center=�5     $��Center=�61     H1#���  5     
Overview6?   �� 5  
   $��6��6   $��0��6!   $��20��61   $��2��6F    J� Opacity$	 0.6Q�5  :   #���  5     #���  5     $��Center=�5     $��Center=�6/   ��5     �� *LastActionTextHelper+5  N   �� 5  
   $��2��6   $��22��6"   $��4��63   $��5��6G   $��Center=�6Y   $ Vertical=�6t   .+�5     ��   MainButtonStyle#���  6   $��40��5     ��5     �� *StartFilterAudienceCommand+��5     �� *ButtonContent+�� 5     -   5         ��Name$ Status��5     $��Center=�5     
�������$��0,20,0,5q�5       
	 Bigger#��� 	 5!      
 TextBlockThinStyle#��� 
 5"     ��5#     �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction    Triggers  5%     /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  5&     !   Value$! WORKING��6$   :  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger"   Binding" 64   �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.TriggerBase#   Actions.# 5'     <	  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction	  5
  /Microsoft.Xaml.Behaviors.Media.StoryboardAction$ 
  
Storyboard#$ [ 64   % 	  ControlStoryboardOption$
% Play=�6]   5(       5)     $	! OFF��6$   " 60   �� *Stats.Status+.# 5*     	  #$ [ 64   $
% Stop=�6]   5+     5-     5.     �� 51  
   $��1��6   $��15��6    $��0��62   $��20��6B   
������ $��0q�53     #���   55     ��.  $��Hidden=�57     $��Hidden=�58     !& �� IsDeferredScrollingEnabled& .  ��54     �� ' �� Source#' [ 5:     5;     5<     