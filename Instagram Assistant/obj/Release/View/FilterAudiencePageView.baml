   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel   Instagram Assistant5   /Instagram_Assistant.View.FilterAudiencePageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      @d2http://schemas.microsoft.com/expression/blend/2008      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � 	Resources     AudienceAction  BorderColor �� CornerRadius �� Orientation j� Stretch �� ImageSource!  FontAwesome.WPF.FontAwesome   Icon � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  audienceCollection �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction  	FlashAnim �� 
TargetName W� From	 W� To g� AutoReverse g� RepeatBehavior  	ColorAnim  SecondColor	 �� To��%�  &	        7  &	 �    &	 �    &	 $    �� .R��� 8��  $	��550��$ 5��._��� $ 
Horizontal=�.+��� $ Vertical=�$	��150��$��Center=�.+�A� $��40��$��40��$��2��
����������� $ Uniform=� �� *AccountImage+�� $��5,0,0,0q�
�������$��10(�$��Center=�$��Center=�$��
ExtraLight"����� *AccountName+  $��2��$��16(�$��0,5,0,0q����� *ActionColor+ �� *Action+�� $ Vertical=�$��0,5,0,5q�$	��220��.+��� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Type: ��
�������$��12(�$��55��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *AccountType+���� *AccountTypeColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��
Categoty: ��
�������$��12(�$��55��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *AccountCategory+���� *AccountCategoryColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��City:  ��
�������$��12(�$��55��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *City+���� *	CityColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Image:  ��
�������$��12(�$��55��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *
HasImage+���� *HasImageColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Bio:  ��
�������$��12(�$��55��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *HasBio+���� *HasBioColor+�� $ Vertical=�$��0,5,0,5q�$	��150��.+��� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Followers Count:  ��
�������$��12(�$��90��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *FollowersCount+���� *FollowersCountColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Media Count:  ��
�������$��12(�$��90��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *
MediaCount+���� *MediaCountColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Highlight:  ��
�������$��12(�$��90��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *HasHighlight+���� *HasHighlightColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��Stop word:  ��
�������$��12(�$��90��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *HasStopWord+���� *HasStopWordColor+�� $ 
Horizontal=�.+��� $��5,0,0,0q�$��
Go word:  ��
�������$��12(�$��90��$��Center=�$��
ExtraLight"��� $��5,0,0,0q�$��12(�$��Center=�$��
ExtraLight"����� *	HasGoWord+���� *HasGoWordColor+ :� $ Loaded��[��� .���� .��t� $ Margin�$	 
-100,0,0,0X�$
 0X�$	 0.9Q�$ 0:0:0.5D�   $ EaseOut=�$ 2��$ 7Q���  �� $ AudienceActions�   $
 Time��$ 
Descending=��� .��W� $ Status��$ Opacity�$ 1X�$	 0.3X� . $ 0:0:1D�$ Forever���� .���� $ Status��$( "Foreground.(SolidColorBrush.Color)�$ Forever�� . $ 0:0:1D�# �  .���  � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��22��$��4��$��2��$��0��  TextBlockMainStyle#���  $��Center=�$��Center=�  H1#���  
Overview�� $��6��$��0��$��20��$��2�� J� Opacity$	 0.6Q�#���  #���  $��Center=�$��Center=����� *LastActionTextHelper+�� $��2��$��22��$��4��$��5��$��Center=�$ Vertical=�.+���   MainButtonStyle#���  $��40������ *StartFilterAudienceCommand+���� *ButtonContent+�� -    ��Name$ Status��$��Center=�
�������$��0,20,0,5q� 
	 Bigger#��� 	  
 TextBlockThinStyle#��� 
 ���� *Stats.Status+*  $Microsoft.Xaml.Behaviors.Interaction    Triggers  /  )Microsoft.Xaml.Behaviors.Core.DataTrigger  !   Value$! WORKING��:  4Microsoft.Xaml.Behaviors.Core.PropertyChangedTrigger"   Binding" �� *Stats.Status+*  $Microsoft.Xaml.Behaviors.TriggerBase#   Actions.# <	  6Microsoft.Xaml.Behaviors.Media.ControlStoryboardAction	  5
  /Microsoft.Xaml.Behaviors.Media.StoryboardAction$ 
  
Storyboard#$ [ % 	  ControlStoryboardOption$
% Play=�  $	! OFF��" �� *Stats.Status+.# 	  #$ [ $
% Stop=��� $��1��$��15��$��0��$��20��
������ $��0q�#���   ��.  $��Hidden=�$��Hidden=�!& �� IsDeferredScrollingEnabled& .  ���� ' �� Source#' [ 