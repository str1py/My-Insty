   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel   Instagram Assistant/   )Instagram_Assistant.View.UnfollowPageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      @d2http://schemas.microsoft.com/expression/blend/2008      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � 	Resources     UnfollowAction  BorderColor �� CornerRadius �� Orientation j� Stretch �� ImageSource!  FontAwesome.WPF.FontAwesome   Icon � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  unfollowCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%�  &	        7  &	 V    &	 �    &	 �    �� .R��� 8��  $ 5��._��� $	��480��$��50��$ 
Horizontal=�.+�A� $��10,0,0,0q�$��25��$��25��$��2��
����������� $ Uniform=� �� *AccountImage+�� $��5,0,0,0q�
�������$��12(�$	��200��$��Center=�$��
ExtraLight"����� *AccountName+�� 
�������$��12(�$	��100��$��Center=�$��
ExtraLight"����� *Status+  $��2��$��Center=�$��20(�$��	100,0,0,0q�
������� �� *Action+ :� $ Loaded��[��� .���� .��t� $ Margin�$	 
-100,0,0,0X�$
 0X�$	 0.9Q�$ 0:0:0.5D�   $ EaseOut=�$ 2��$ 7Q���  �� $ Actions�   $
 Time��$ 
Descending=��� .��W� $ Status��$ Opacity�$ 1X�$	 0.3X� . $ 0:0:1D�$ Forever���� .���� $ Status��$( "Foreground.(SolidColorBrush.Color)�$ Forever�� . $ 0:0:1D�# �  .���  � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��21��$��21��$��2��$��0��  TextBlockMainStyle#���  $��Center=�  H1#���  $��Overview���� $��0��$��5��$��2��$��0��#���    Regular#���  $��Last actions���� $��21��$��13��$��6��$��3��
�������$ 3�� J� Opacity$
 0.05Q� J� Effect -  'System.Windows.Media.Effects.BlurEffect      Radius$  10Q�!   
KernelType$! Gaussian=��� $��21��$��11��$��11��$��3��
�������$ 3��$
 0.05Q�   $  10Q�$! Gaussian=��� $��2��$��24��$��5��$��3��$��Center=�$ Vertical=�.+���  	 MainButtonStyle#��� 	 $��40������ *StartCommand+���� *ButtonContent+�� -   " ��Name$" Status��$��Center=�
�������$��0,20,0,5q� 

 Bigger#��� 
   TextBlockThinStyle#���  ���� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction#   Triggers# /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  $   Value$$ WORKING��:  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger%   Binding% �� *Stats.Status+*	  $Microsoft.Xaml.Behaviors.TriggerBase& 	  Actions.& <
  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction
  5  /Microsoft.Xaml.Behaviors.Media.StoryboardAction'   
Storyboard#' [ ( 
  ControlStoryboardOption$
( Play=�  $	$ OFF��% �� *Stats.Status+.& 
  #' [ $
( Stop=��� $��6��$��3��$��22��$��9��$��Center=�$ 
Horizontal=�.+��� $��Center=�.+��� $ 
Horizontal=�.+��� $ Vertical=�$��0,0,40,0q�.+��� $ 
Horizontal=�.+�  $��0,0,5,0q�$ 	UserTimes=�$��15(�
�������$��Center=�$��Center=��� $��Center=�
�������#��� 
 #���  ���� *Stats.SessionCount+�� $��Center=�#���  	Session�� $ Vertical=�$��0,0,40,0q�.+��� $ 
Horizontal=�.+�  $��0,0,5,0q�$ 	UserTimes=�$��15(�
�������$��Center=�$��Center=��� $��Center=�
�������#��� 
 #���  ���� *Stats.Count+�� $��Center=�#���  Total�� $��Center=�.+��� $��Center=�
�������#��� 
 #���  ���� *Stats.NextIn+�� $��Center=�#���  	Next in�� $��11��$��3��$��21��$��13��$��Center=�$��Center=�$ 
Horizontal=�.+��� $��Center=�$��0,0,30,0q�.+��� $��Center=�
�������$��0,0,0,5q�#��� 
 #���  ���� *Stats.TimeInWork+�� $��Center=�#���  Time in work�� $��Center=�.+��� $��Center=�
�������$��0,0,0,5q�#��� 
 #���  ���� *Stats.NextSessionIn+�� $��Center=�#���  Rest�� $��5��$��0��$��20��$��2��$	 0.6Q�#���  #���  $��Center=�$��Center=����� *LastActionTextHelper+�� $��1��$��13��$��0��$��20��
������ $��0q�#���   ��.  $��Hidden=�$��Hidden=�!) �� IsDeferredScrollingEnabled) .  ���� * �� Source#* [ 